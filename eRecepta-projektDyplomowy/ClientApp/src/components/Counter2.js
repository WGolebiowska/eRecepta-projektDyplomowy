import React, { Component } from 'react'
// import { Recepta } from '../containers/recepta/Recepta';
import { Recepta } from '../containers'
// import { CustomForm } from './CustomForm'
import CustomForm from './CustomForm2'

export class Counter2 extends Component {
  static displayName = Counter2.name

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
        <h1>eKonsultacja</h1>

        <p class="fw-bold"> Żeby umówić się z lekarzem wypełnij ankiete</p>
        <CustomForm />
        {/* 
        <p aria-live="polite">Current count: <strong>{this.state.currentCount}</strong></p>

        <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button> */}
      </div>
    )
  }
}
