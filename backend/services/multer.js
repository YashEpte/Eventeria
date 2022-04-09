const multer = require('multer');
const { extname, resolve } = require('path');

const diskStorage = multer.diskStorage({
  destination: (req, file, done) => {
    if (!file) return done(new Error('Upload file error'), null);
    return done(null, resolve('assets/images'));
  },
  filename: (req, file, done) => {
    if (file) {
      const imagePattern = /(jpg|jpeg|png|gif|svg)/gi;
      const mathExt = extname(file.originalname).replace('.', '');

      if (!imagePattern.test(mathExt)) {
        return new TypeError('File format is not valid');
      }

      req.file = file.originalname;
      return done(null, file.originalname);
    }
  },
});

const fileUpload = multer({ storage: diskStorage, limits: 1000000 });

module.exports = fileUpload;
