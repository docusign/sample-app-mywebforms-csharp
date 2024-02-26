import React from 'react';
import { GeneralSection, FeaturesSection, ActionSection } from './components';

export const Home = () => {
  return (
    <div className='home-page'>
      <section className='home-main-section'>
        <div className='home-main'>
          <div className='container'>
            <GeneralSection />
            <FeaturesSection />
          </div>
        </div>
      </section>
      <section className='action-section'>
        <div className='container'>
          <ActionSection />
        </div>
      </section>
    </div>
  );
};
