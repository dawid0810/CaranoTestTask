import axios from "axios";
import {SETTINGS} from "./settings";

export const call = (values) => {
  return axios.post(
    SETTINGS.HOST_URL,
    {Price: Number(values.price.replace(',', '.').replace(' ',''))},
    {
      headers: {'Content-Type': 'application/json'},
      timeout: 5000
    }
  )
};

export const validate = (values) => {
  const errors = {};
  if (!values.price) {
    errors.price = 'Required';
  } else if (
    !/^\d+(,\d{1,2})?$/i.test(values.price.replace(' ', ''))
  ) {
    errors.price = 'Invalid price format';
  } else if (
    Number(values.price.replace(',', '.')) > 999999999
  ) {
    errors.price = "Price cannot be bigger than 999 999 999"
  }
  return errors;
};