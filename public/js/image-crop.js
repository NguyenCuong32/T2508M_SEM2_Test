(function () {
  const fileInput = document.getElementById('fileInput');
  if (!fileInput) return;

  const browseBtn = document.getElementById('browseBtn');
  const imageDisplay = document.getElementById('imageDisplay');
  const imageHidden = document.getElementById('image');
  const ratioHidden = document.getElementById('imageRatio');
  const previewImg = document.getElementById('previewImg');
  const previewPlaceholder = document.getElementById('previewPlaceholder');
  const previewFrame = document.getElementById('previewFrame');

  const modal = document.getElementById('cropModal');
  const viewport = document.getElementById('cropViewport');
  const cropImage = document.getElementById('cropImage');
  const zoomSlider = document.getElementById('cropZoom');
  const ratioButtons = document.querySelectorAll('.ratio-btn');
  const cancelBtn = document.getElementById('cropCancelBtn');
  const confirmBtn = document.getElementById('cropConfirmBtn');
  const cropSizeBar = document.getElementById('cropSizeBar');
  const cropSizeText = document.getElementById('cropSizeText');
  const imageSizeBar = document.getElementById('imageSizeBar');
  const imageSizeText = document.getElementById('imageSizeText');

  const MAX_ORIGINAL_SIZE = 10 * 1024 * 1024; // 10MB - giới hạn ảnh gốc trước khi crop
  const OUTPUT_SIZE_REFERENCE = 2 * 1024 * 1024; // 2MB - mốc tham chiếu cho thanh đo ảnh đã crop

  function formatBytes(bytes) {
    if (bytes < 1024) return bytes + ' B';
    if (bytes < 1024 * 1024) return (bytes / 1024).toFixed(1) + ' KB';
    return (bytes / (1024 * 1024)).toFixed(2) + ' MB';
  }

  function dataUrlSize(dataUrl) {
    const base64 = dataUrl.split(',')[1] || '';
    const padding = (base64.match(/=*$/) || [''])[0].length;
    return Math.max(0, Math.round((base64.length * 3) / 4) - padding);
  }

  function updateSizeMeter(barEl, textEl, bytes, maxBytes, label) {
    if (!barEl || !textEl) return;
    const ratio = Math.min(1, bytes / maxBytes);
    barEl.style.width = ratio * 100 + '%';
    barEl.classList.remove('size-bar-warn', 'size-bar-danger');
    if (ratio >= 1) barEl.classList.add('size-bar-danger');
    else if (ratio >= 0.7) barEl.classList.add('size-bar-warn');
    textEl.textContent = label + ': ' + formatBytes(bytes);
  }

  let currentRatio = (ratioHidden && ratioHidden.value) || '1:1';
  let naturalWidth = 0;
  let naturalHeight = 0;
  let scale = 1;
  let offsetX = 0;
  let offsetY = 0;
  let dragging = false;
  let dragStartX = 0;
  let dragStartY = 0;
  let startOffsetX = 0;
  let startOffsetY = 0;
  let pendingFilename = '';

  function ratioToWH(ratio) {
    const [a, b] = ratio.split(':').map(Number);
    return { w: a, h: b };
  }

  function viewportSizeForRatio(ratio) {
    const { w, h } = ratioToWH(ratio);
    const maxDim = 320;
    if (w >= h) {
      return { width: maxDim, height: Math.round((maxDim * h) / w) };
    }
    return { width: Math.round((maxDim * w) / h), height: maxDim };
  }

  function clampOffset() {
    const vw = viewport.clientWidth;
    const vh = viewport.clientHeight;
    const scaledW = naturalWidth * scale;
    const scaledH = naturalHeight * scale;
    const minX = vw - scaledW;
    const minY = vh - scaledH;
    offsetX = Math.min(0, Math.max(minX, offsetX));
    offsetY = Math.min(0, Math.max(minY, offsetY));
  }

  function updateImageTransform() {
    cropImage.style.width = naturalWidth * scale + 'px';
    cropImage.style.height = naturalHeight * scale + 'px';
    cropImage.style.transform = 'translate(' + offsetX + 'px, ' + offsetY + 'px)';
  }

  function centerImage() {
    const vw = viewport.clientWidth;
    const vh = viewport.clientHeight;
    offsetX = (vw - naturalWidth * scale) / 2;
    offsetY = (vh - naturalHeight * scale) / 2;
    clampOffset();
    updateImageTransform();
  }

  function recomputeMinScale() {
    const vw = viewport.clientWidth;
    const vh = viewport.clientHeight;
    const minScale = Math.max(vw / naturalWidth, vh / naturalHeight);
    scale = minScale;
    zoomSlider.min = minScale.toFixed(4);
    zoomSlider.max = (minScale * 3).toFixed(4);
    zoomSlider.step = 0.001;
    zoomSlider.value = scale.toFixed(4);
  }

  function applyRatio(ratio) {
    currentRatio = ratio;
    const { width, height } = viewportSizeForRatio(ratio);
    viewport.style.width = width + 'px';
    viewport.style.height = height + 'px';
    ratioButtons.forEach((btn) => btn.classList.toggle('active', btn.dataset.ratio === ratio));
    recomputeMinScale();
    centerImage();
  }

  ratioButtons.forEach((btn) => {
    btn.addEventListener('click', () => applyRatio(btn.dataset.ratio));
  });

  zoomSlider.addEventListener('input', () => {
    scale = parseFloat(zoomSlider.value);
    clampOffset();
    updateImageTransform();
  });

  viewport.addEventListener('mousedown', (e) => {
    dragging = true;
    dragStartX = e.clientX;
    dragStartY = e.clientY;
    startOffsetX = offsetX;
    startOffsetY = offsetY;
  });

  window.addEventListener('mousemove', (e) => {
    if (!dragging) return;
    offsetX = startOffsetX + (e.clientX - dragStartX);
    offsetY = startOffsetY + (e.clientY - dragStartY);
    clampOffset();
    updateImageTransform();
  });

  window.addEventListener('mouseup', () => {
    dragging = false;
  });

  if (browseBtn) {
    browseBtn.addEventListener('click', () => fileInput.click());
  }

  fileInput.addEventListener('change', (e) => {
    const file = e.target.files[0];
    if (!file) return;

    if (file.size > MAX_ORIGINAL_SIZE) {
      alert('Ảnh quá lớn (' + formatBytes(file.size) + '). Vui lòng chọn ảnh dưới ' + formatBytes(MAX_ORIGINAL_SIZE) + '.');
      fileInput.value = '';
      return;
    }

    pendingFilename = file.name;
    updateSizeMeter(cropSizeBar, cropSizeText, file.size, MAX_ORIGINAL_SIZE, 'Ảnh gốc');

    const reader = new FileReader();
    reader.onload = (ev) => {
      cropImage.onload = () => {
        naturalWidth = cropImage.naturalWidth;
        naturalHeight = cropImage.naturalHeight;
        modal.hidden = false;
        applyRatio(currentRatio);
      };
      cropImage.src = ev.target.result;
    };
    reader.readAsDataURL(file);
  });

  cancelBtn.addEventListener('click', () => {
    modal.hidden = true;
    fileInput.value = '';
  });

  confirmBtn.addEventListener('click', () => {
    const { width, height } = viewportSizeForRatio(currentRatio);
    const outputScale = 2;
    const canvas = document.createElement('canvas');
    canvas.width = width * outputScale;
    canvas.height = height * outputScale;
    const ctx = canvas.getContext('2d');
    ctx.drawImage(
      cropImage,
      -offsetX / scale,
      -offsetY / scale,
      width / scale,
      height / scale,
      0,
      0,
      canvas.width,
      canvas.height
    );
    const dataUrl = canvas.toDataURL('image/jpeg', 0.9);

    imageHidden.value = dataUrl;
    if (ratioHidden) ratioHidden.value = currentRatio;
    if (imageDisplay) imageDisplay.value = pendingFilename;

    if (previewImg) {
      previewImg.src = dataUrl;
      previewImg.style.display = 'block';
    }
    if (previewPlaceholder) previewPlaceholder.style.display = 'none';
    if (previewFrame) previewFrame.style.aspectRatio = currentRatio.split(':').join(' / ');

    updateSizeMeter(imageSizeBar, imageSizeText, dataUrlSize(dataUrl), OUTPUT_SIZE_REFERENCE, 'Ảnh sau khi cắt');

    modal.hidden = true;
    fileInput.value = '';
  });

  if (imageDisplay) {
    imageDisplay.addEventListener('input', () => {
      imageHidden.value = imageDisplay.value;
      if (ratioHidden) ratioHidden.value = '';
      if (previewFrame) previewFrame.style.aspectRatio = '1 / 1';
      if (previewImg) {
        if (imageDisplay.value) {
          previewImg.src = imageDisplay.value;
          previewImg.style.display = 'block';
          if (previewPlaceholder) previewPlaceholder.style.display = 'none';
        } else {
          previewImg.style.display = 'none';
          if (previewPlaceholder) previewPlaceholder.style.display = 'flex';
        }
      }
      if (imageSizeBar && imageSizeText) {
        if (imageDisplay.value) {
          imageSizeBar.style.width = '0%';
          imageSizeBar.classList.remove('size-bar-warn', 'size-bar-danger');
          imageSizeText.textContent = 'Đường dẫn thủ công (không xác định dung lượng)';
        } else {
          imageSizeBar.style.width = '0%';
          imageSizeBar.classList.remove('size-bar-warn', 'size-bar-danger');
          imageSizeText.textContent = 'Chưa có ảnh';
        }
      }
    });
  }

  if (imageHidden && imageHidden.value) {
    if (imageHidden.value.startsWith('data:')) {
      updateSizeMeter(imageSizeBar, imageSizeText, dataUrlSize(imageHidden.value), OUTPUT_SIZE_REFERENCE, 'Ảnh hiện tại');
    } else if (imageSizeText) {
      imageSizeText.textContent = 'Ảnh hiện tại (đường dẫn, không xác định dung lượng)';
    }
  }
})();
