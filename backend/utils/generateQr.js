const QRCode = require('qrcode');

const generateQr = ({ code }) => {
  return new Promise((resolve, reject) => {
    QRCode.toDataURL(`${code}`, function (err, url) {
      if (err) {
        reject(err);
      }

      resolve(url);
    });
  });
};

module.exports = generateQr;
