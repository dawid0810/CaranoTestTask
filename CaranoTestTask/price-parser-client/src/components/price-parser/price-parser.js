import React from "react";
import PriceParserHeader from './price-parser-header/price-parser-header.js';
import PriceParserForm from './price-parser-form/price-parser-form.js';
import QueryResultPanel from '../query-result-panel/query-result-panel.js';

class PriceParser extends React.Component {
  handleResult = parsedPrice => {
    this.setState({result: parsedPrice});
  };

  constructor(props) {
    super(props);
    this.state = {
      result: null,
    };
  }

  render() {
    return (
      <div>
        <PriceParserHeader />
        <PriceParserForm onResult={parsedPrice => this.handleResult(parsedPrice)}/>
        <QueryResultPanel result={this.state.result} />
      </div>
    );
  }
}

export default PriceParser;