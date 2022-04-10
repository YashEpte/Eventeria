console.clear();

const dotenv = require('dotenv');
const express = require('express');
const mongoose = require('mongoose');
dotenv.config();

mongoose
  .connect(process.env.DATABASE)
  .then(() => {
    console.log('database connected');
  })
  .catch((err) => {
    console.log(err);
  });

const app = express();

app.use(express.json());
app.use('/images', express.static('assets/images'));
app.use(express.urlencoded({ extended: true }));

app.use('/', require('./routes'));

app.use((err, _, res, next) => {
  res.status(403).send({
    status: 'error',
    error: err.message || 'Something went wrong',
  });
});

const port = process.env.PORT || 8000;
app.listen(port, (err) => {
  console.log(err ? err : `Listening on port ${port}`);
});
