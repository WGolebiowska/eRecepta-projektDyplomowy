import React, { Component } from 'react'
import DoctorPrescriptionForm from './DoctorPrescription'

export class DoctorPrescription extends Component {
  static displayName = DoctorPrescription.name

  constructor(props) {
    super(props)
  }

  render() {
    return (
      <div class="text-center">
        <h1>Nowa eRecepta</h1>

        <p class="fw-bold">Aby wystawić nową eReceptę, wypełnij formularz</p>
        <DoctorPrescriptionForm />
      </div>
    )
  }
}
