import {ErrorMessage, Field, Form, Formik} from "formik";
import axios from "axios";
import React from "react";
import './price-parser-form.css';

const PriceParserForm = props => {
  return (
    <div>
      <Formik
        validateOnChange={true}
        validateOnBlur={false}
        initialValues={{price: ''}}
        validate={values => {
          const errors = {};
          if (!values.price) {
            errors.price = 'Required';
          } else if (
            !/^\d+(,\d{1,2})?$/i.test(values.price)
          ) {
            errors.price = 'Invalid price format';
          } else if (
            Number(values.price.replace(',', '.')) > 999999999
          ) {
            errors.price = "Price cannot be bigger than 999 999 999"
          }
          return errors;
        }}
        onSubmit={(values, {setSubmitting}) => {
          setSubmitting(true);
          axios.post(
            'https://localhost:44355/api/PriceParser',
            {PriceString: values.price},
            {
              headers: {'Content-Type': 'application/json'},
              timeout: 5000
            }
          ).then(response => {
            props.onResult(response.data);
          })
            .catch(reason => {
              console.log(reason);
              props.onResult("There was an issue with processing your request. Please try again.");
            })
            .finally(() => setSubmitting(false));
        }}
      >
        {({isSubmitting, handleChange, handleBlur, handleSubmit, errors}) => {
          let hasErrors = Object.keys(errors).length > 0;
          return (
          <Form onSubmit={handleSubmit} autoComplete="off">
            <Field 
              type="text" 
              name="price" 
              placeholder="Enter price..."
              className={errors.price && "error"}
              onChange={(...args) => {
                handleChange(...args);
                handleBlur(...args);
              }}/>
            <ErrorMessage name="price" component="div" className="error-message"/>
            <button type="submit" disabled={isSubmitting || hasErrors} className={hasErrors ? "error" : ""}>
              Parse
            </button>
          </Form>
          );}
        }
      </Formik>
    </div>
  );
};

export default PriceParserForm;