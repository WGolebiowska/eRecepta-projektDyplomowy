import React from 'react';
import Feature from '../../components/feature/Feature';
import './personel.css';
import dr1 from '../../assets/dr1.png';
import dr2 from '../../assets/dr2.png';
import dr3 from '../../assets/dr3.png';

const WhatGPT3 = () => (
  <div className="gpt3__whatgpt31 section__margin" id="wgpt3">
    <div className="gpt3__whatgpt3-heading">
      <p>O komfort Twojej eWizyty zadba nasz wyszkolony personel</p>
    </div>
    <div className="gpt3__whatgpt3-container">
      {/* <Feature title="Chatbots" text="We so opinion friends me message as delight. Whole front do of plate heard oh ought." /> */}
      <Feature text="Dr. George" title={<img src={dr1}/>} />
      <Feature text="Dr. Artur" title={<img src={dr2}/>} />
      <Feature text="Dr. Freddie" title={<img src={dr3}/>} />
    </div>
  </div>
);

export default WhatGPT3;
