window.imageResizer = {
    resize: (base64, maxWidth, maxHeight) => {
      return new Promise((resolve, reject) => {
        const img = new Image();
        img.src = base64;
        img.onload = () => {
          let width = img.width;
          let height = img.height;
  
          // Simple max-dimension logic (optional)
          if (width > maxWidth) {
            height = Math.round(height * (maxWidth / width));
            width = maxWidth;
          }
          if (height > maxHeight) {
            width = Math.round(width * (maxHeight / height));
            height = maxHeight;
          }
  
          const canvas = document.createElement('canvas');
          canvas.width = width;
          canvas.height = height;
          const ctx = canvas.getContext('2d');
          ctx.drawImage(img, 0, 0, width, height);
  
          // 0.5 for 50% quality (JPEG)
          const resizedBase64 = canvas.toDataURL('image/jpeg', 0.4);
          resolve(resizedBase64);
        };
        img.onerror = reject;
      });
    }
  };