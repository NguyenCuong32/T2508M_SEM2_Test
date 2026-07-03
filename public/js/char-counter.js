(function () {
  function bindCounter(inputId, counterId) {
    const input = document.getElementById(inputId);
    const counter = document.getElementById(counterId);
    if (!input || !counter) return;

    const max = parseInt(input.getAttribute('maxlength'), 10) || 0;

    const update = () => {
      counter.textContent = input.value.length + '/' + max;
      counter.classList.toggle('char-counter-limit', input.value.length >= max);
    };

    input.addEventListener('input', update);
    update();
  }

  bindCounter('treename', 'treenameCounter');
  bindCounter('description', 'descriptionCounter');
})();
