const nodemailer = require('nodemailer');

const transporter = nodemailer.createTransport({
  service: 'gmail',
  auth: {
    user: 'adarshco077@gmail.com',
    pass: 'dfflbsavdyfdsjdx',
  },
});

module.exports = (customOptions) =>
  new Promise((resolve, reject) => {
    const mailOptions = {
      from: 'Eventaria <adarshco077@gmail.com>',
      ...customOptions,
    };

    transporter.sendMail(mailOptions, (err) => (err ? reject(err) : resolve()));
  });
