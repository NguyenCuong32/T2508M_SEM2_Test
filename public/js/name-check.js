(function () {
  const treenameInput = document.getElementById('treename');
  const msgEl = document.getElementById('treenameCheckMsg');
  if (!treenameInput || !msgEl) return;

  const excludeId = treenameInput.getAttribute('data-exclude-id') || '';
  let debounceTimer = null;
  let requestToken = 0;

  function setMessage(text, isDuplicate) {
    msgEl.textContent = text;
    msgEl.classList.toggle('name-check-msg-duplicate', !!isDuplicate);
  }

  function checkName() {
    const value = treenameInput.value.trim();
    clearTimeout(debounceTimer);

    if (!value) {
      setMessage('', false);
      return;
    }

    const token = ++requestToken;
    const params = new URLSearchParams({ name: value });
    if (excludeId) params.set('excludeId', excludeId);

    fetch('/api/check-name?' + params.toString())
      .then((res) => res.json())
      .then((data) => {
        if (token !== requestToken) return;
        if (data.exists) {
          setMessage('Tên cây này đã tồn tại trong danh sách.', true);
        } else {
          setMessage('Tên cây hợp lệ, chưa trùng.', false);
        }
      })
      .catch(() => {});
  }

  treenameInput.addEventListener('input', () => {
    clearTimeout(debounceTimer);
    if (!treenameInput.value.trim()) {
      setMessage('', false);
      return;
    }
    debounceTimer = setTimeout(checkName, 350);
  });

  treenameInput.addEventListener('blur', () => {
    clearTimeout(debounceTimer);
    checkName();
  });
})();
