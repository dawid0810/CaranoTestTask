import {ErrorMessage, Field, Form, Formik} from "formik";
import React from "react";
import './price-parser-form.css';
import { call, validate } from './price-parser-form-functions';

const PriceParserForm = props => {
  return (
      <Formik
        validateOnChange={true}
        validateOnBlur={false}
        initialValues={{price: ''}}
        validate={values => validate(values)}
        onSubmit={(values, {setSubmitting}) => {
          setSubmitting(true);
          call(values).then(response => {
            props.onResult(response.data);
          }).catch(reason => {
            console.log(reason);
            props.onResult("There was an issue with processing your request. Please try again.");
          }).finally(() => setSubmitting(false));
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
  );
};

export default PriceParserForm;