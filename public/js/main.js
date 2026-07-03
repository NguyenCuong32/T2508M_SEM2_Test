document.addEventListener("DOMContentLoaded", () => {
  const preview = document.querySelector("#previewImage");
  const imageText = document.querySelector("#image");
  const imageFile = document.querySelector("#imageFile");
  const placeholder = preview?.dataset.placeholder || "/images/tree-placeholder.png";

  document.querySelectorAll("img[data-placeholder]").forEach((image) => {
    image.addEventListener("error", () => {
      image.src = image.dataset.placeholder;
    });
  });

  document.querySelectorAll("form[data-confirm]").forEach((form) => {
    form.addEventListener("submit", (event) => {
      if (!window.confirm(form.dataset.confirm)) {
        event.preventDefault();
      }
    });
  });

  if (imageFile && imageText && preview) {
    imageFile.addEventListener("change", () => {
      const file = imageFile.files && imageFile.files[0];

      if (!file) {
        return;
      }

      imageText.value = file.name;
      preview.src = URL.createObjectURL(file);
    });
  }

  if (imageText && preview) {
    imageText.addEventListener("input", () => {
      const value = imageText.value.trim();

      if (!value) {
        preview.src = placeholder;
        return;
      }

      if (value.startsWith("/") || value.startsWith("http://") || value.startsWith("https://")) {
        preview.src = value;
      }
    });
  }
});
