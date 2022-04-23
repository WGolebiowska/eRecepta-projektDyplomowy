import React from 'react';
import Feature from '../../components/feature/Feature';
import './whatGPT3.css';
import step1 from '../../assets/step1.png';
import step2 from '../../assets/step2.png';
import step3 from '../../assets/step3.png';


const WhatGPT3 = () => (
  <div className="gpt3__whatgpt3 section__margin" id="wgpt3">
    <div className="gpt3__whatgpt3-heading">
      <h1 className="gradient__textl">Całą operację przeprowadzisz w 3 prostych krokach</h1>
    </div>
    <div className="gpt3__whatgpt3-container1">
      <Feature title={<img src={step1} />} text="Wypełnij ankietę eKonsultacji lub eRecepty" />
      <Feature title={<img src={step2} />} text="Opłać poradę medyczną" />
      <Feature title={<img src={step3} />} text="Receptę otrzymasz SMSem oraz mailem" />
    </div>
  </div>
);

export default WhatGPT3;
