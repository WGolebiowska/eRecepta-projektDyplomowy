import React from 'react'
import Feature from '../../components/feature/Feature'
import './konsultacje.css'
import logo from '../../assets/numeric.png'

const featuresData = [
  {
    title: <img src={logo} />,
    text: 'System obsługi pozwala w którkim czasie realizować recepty.',
  },
  {
    title: <img src={logo} />,
    text:
      'bez kolejek bez wychodzenia z domu bez narażania się na kolejne choroby',
  },
  {
    title: <img src={logo} />,
    text:
      'bez eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeekolejek bez wychodzenia z domu bez narażania się na kolejne choroby',
  },
]

const Features = () => (
  <div className="gpt3__features section__padding">
    <div>
      <div className="gpt3__features-container">
        {featuresData.map((item, index) => (
          <Feature
            title={item.title}
            text={item.text}
            key={item.title + index}
          />
        ))}
      </div>
      <div className="gpt3__recepta-btn" id="ekonsultacja">
        <button type="button">zamów eReceptę</button>
      </div>
    </div>
  </div>
)

export default Features
