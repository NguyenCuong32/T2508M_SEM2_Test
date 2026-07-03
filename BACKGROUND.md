# Prompt thiết kế ảnh nền (Background) - Tree Shop

Chủ đề: **vườn cây trong nhà kính (greenhouse garden), render 3D, phong cách fantasy**, tông sáng để hợp với hiệu ứng kính mờ (liquid glass) của header/footer/component trên web. Bảng màu tham chiếu (`public/css/style.css`):

- Xanh lá rừng: `#4a7c59`
- Nâu gỗ: `#8b5e34`
- Kem nền: `#f7f1e3`

## Prompt chính (dùng cho Midjourney / DALL·E / Stable Diffusion)

```
A bright fantasy 3D render of a magical greenhouse garden, tall glass dome ceiling
glowing with soft daylight, lush glowing plants and small enchanted trees,
floating leaves and sparkling light particles, warm wooden plant shelves and
stone pathways, whimsical oversized flowers, pastel green and warm wood color
palette, airy and luminous atmosphere, soft volumetric sunbeams, high-key
lighting, no dark shadows, Pixar-like stylized 3D render, octane render,
clean and uncluttered composition with open space for UI overlay,
wide banner aspect ratio, website background image, no people, no text
```

## Prompt phụ (biến thể tối giản hơn, sáng và ít chi tiết hơn)

```
Bright whimsical 3D fantasy greenhouse background, soft pastel green and cream
lighting, glowing potted plants, glass ceiling with sunlight, minimal blurred
depth of field, dreamy soft-focus atmosphere, plenty of open negative space,
stylized low-poly or Pixar-style render, website hero background, no text
```

## Negative prompt (loại trừ)

```
no text, no watermark, no people, no faces, no harsh dark shadows,
no oversaturated neon colors, not photorealistic gritty texture,
no busy cluttered foreground, no horror or dark fantasy elements
```

## Thông số gợi ý

| Thông số | Giá trị |
|---|---|
| Tỉ lệ khung hình | 16:9 hoặc 21:9 (banner ngang cho hero/background full-width) |
| Độ phân giải | tối thiểu 1920x1080px |
| Độ sáng | tông sáng cao (high-key), tránh vùng tối gắt để chữ/kính mờ luôn rõ |
| Định dạng lưu | `.jpg`/`.png` (đã có `public/images/background.png`) |

## Cách áp dụng vào CSS

```css
body {
  background: url('/images/background.png') center/cover no-repeat fixed;
}
```

*Lưu ý: hiện tại body không dùng lớp phủ gradient (đã bỏ theo yêu cầu trước), ảnh nền hiển thị trực tiếp. Nếu ảnh mới quá tối/chói khiến chữ khó đọc, có thể thêm lại overlay nhẹ dạng `linear-gradient(rgba(247,241,227,0.15), rgba(247,241,227,0.15))` phía trên ảnh.*
