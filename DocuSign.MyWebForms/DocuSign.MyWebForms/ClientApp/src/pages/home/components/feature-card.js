import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';
import { OverlayTrigger, Collapse, Tooltip } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCreditCard, faCar, faShip } from '@fortawesome/free-solid-svg-icons';

const icons = [
  <FontAwesomeIcon className='icon' icon={faCreditCard} />,
  <FontAwesomeIcon className='icon' icon={faCar} />,
  <FontAwesomeIcon className='icon' icon={faShip} />,
];

export const FeatureCard = ({ data, index }) => {
  const { Title, Link: LinkString, FeaturesTitle, Value, Button, Features } = data;
  const [toggle, setToggle] = useState(true);

  return (
    <div className='col-lg-4 panel'>
      <div className='card h-100'>
        <div className='card-title'>
          <h3 className='text-center'>
            {icons[index]}
            {Title}
          </h3>
        </div>
        <div className='card-body'>
          <p className='loan-percentage'>{Value}</p>
          <div className='text-center'>
            <Link to={LinkString} className='btn btn-default'>
              {Button}
            </Link>
          </div>
          <div className='loan-features'>
            <div role='presentation' onClick={() => setToggle((prev) => !prev)}>
              {FeaturesTitle}
            </div>
            <Collapse in={toggle}>
              <ul>
                {Features.map((feature) => {
                  return (
                    <li key={feature.Title}>
                      <OverlayTrigger
                        placement='top'
                        overlay={<Tooltip>{feature.Title}</Tooltip>}
                      >
                        <span>{feature.Text}</span>
                      </OverlayTrigger>
                    </li>
                  );
                })}
              </ul>
            </Collapse>
          </div>
        </div>
      </div>
    </div>
  );
};

FeatureCard.propTypes = {
  data: PropTypes.shape({
    Title: PropTypes.string,
    FeaturesTitle: PropTypes.string,
    Link: PropTypes.string,
    Icon: PropTypes.string,
    Value: PropTypes.string,
    Button: PropTypes.string,
    Features: PropTypes.arrayOf(
      PropTypes.shape({
        Text: PropTypes.string,
        Title: PropTypes.string,
      }),
    ),
  }).isRequired,
  index: PropTypes.number.isRequired,
};
