import React from "react";
import './query-result-panel.css';

const QueryResult = (props) => (
  <div className="result">
    { props.result ? props.result.charAt(0).toUpperCase() + props.result.slice(1) : "Parse the price to see result"}
  </div>
);

export default QueryResult;