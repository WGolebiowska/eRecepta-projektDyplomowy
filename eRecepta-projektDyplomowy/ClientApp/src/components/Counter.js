import React, { Component } from 'react'
// import { Recepta } from '../containers/recepta/Recepta';
import { Recepta } from '../containers'
import CustomForm from './CustomForm'

export class Counter extends Component {
  static displayName = Counter.name

  constructor(props) {
    super(props)
    this.state = { currentCount: 0 }
    this.incrementCounter = this.incrementCounter.bind(this)
  }

  incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount + 1,
    })
  }

  render() {
    return (
      <div class="text-center">
        <h1>eRecepta</h1>

        <p class="fw-bold">Aby zamówić eReceptę, wypełnij ankietę na temat stanu zdrowia</p>
        <CustomForm />
      </div>
    )
  }
}
