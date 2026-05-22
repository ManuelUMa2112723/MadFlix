export function scrollFestivalStrip(element, direction) {
  if (!element) {
    return;
  }

  const distance = Math.max(element.clientWidth * 0.82, 320);
  element.scrollBy({
    left: distance * direction,
    behavior: "smooth"
  });
}
