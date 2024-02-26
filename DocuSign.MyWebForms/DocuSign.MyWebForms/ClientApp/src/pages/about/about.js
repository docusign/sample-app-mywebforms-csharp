import React from 'react';
import { useTranslation } from 'react-i18next';
import parse from 'html-react-parser';

export const About = () => {
  const { t } = useTranslation('About');
  
  return (
    <div className='about-page'>
      <div className='container-md'>
        <h3 id='loanco-sample-app' className='loanco-sample-app'>{parse(t('Header1'))}</h3>
        <blockquote className='description-container'>
          <p>{parse(t('Description1'))}</p>
        </blockquote>
        <hr/>
        <div className='row'>
          <div className='col-lg-6'>
            <p>
              <strong>{parse(t('GitHubLinkTitle'))} 
                <a target='_blank' href={parse(t('GitHubLink'))}
                rel='noreferrer'>{parse(t('GitHubLink'))}</a>
              </strong>
            </p>
  
            <h4 className='features-section-title'>{parse(t('FeaturesSection.Title'))}</h4>
  
            <ul>
              <li>{parse(t('FeaturesSection.Element1'))}</li>
              <li>{parse(t('FeaturesSection.Element2'))}</li>
              <li>{parse(t('FeaturesSection.Element3'))}</li>
              <li>{parse(t('FeaturesSection.Element4'))}</li>
              <li>{parse(t('FeaturesSection.Element5'))}</li>
              <li>{parse(t('FeaturesSection.Element6'))}</li>
              <li>{parse(t('FeaturesSection.Element7'))}</li>
              <li>{parse(t('FeaturesSection.Element8'))}</li>
              <li>{parse(t('FeaturesSection.Element9'))}</li>
              <li>{parse(t('FeaturesSection.Element10'))}</li>
            </ul>
          </div>
          <div className='col-lg-6'>
  
            <h4 className='links-list'>{parse(t('LinksList.Title'))}</h4>  
      
            <h5 className='link-subtitle'>{parse(t('LinksList.Subtitle1'))}</h5>
            <a target='_blank' href={parse(t('LinksList.Link1'))}
            rel='noreferrer'>{parse(t('LinksList.Link1'))}</a>   
    
            <h5 className='link-subtitle'>{parse(t('LinksList.Subtitle2'))}</h5>
            <a target='_blank' href={parse(t('LinksList.Link2'))}
            rel='noreferrer'>{parse(t('LinksList.Link2'))}</a>   
    
            <h5 className='link-subtitle'>{parse(t('LinksList.Subtitle3'))}</h5>
            <a target='_blank' href={parse(t('LinksList.Link3'))}
            rel='noreferrer'>{parse(t('LinksList.Link3'))}</a>   
    
            <h5 className='link-subtitle'>{parse(t('LinksList.Subtitle4'))}</h5> 
            <a target='_blank' href={parse(t('LinksList.Link4'))} rel='noreferrer'>
              {parse(t('LinksList.Link4'))}</a>  
      
          </div>
        </div>
  
        <h3 className='about-docusign'>{parse(t('AboutSection.Title'))}</h3>
        <ul>
          <li>{parse(t('AboutSection.Element1'))}</li>
          <li>{parse(t('AboutSection.Element2'))}</li>
          <li>{parse(t('AboutSection.Element3'))}</li>
          <li>{parse(t('AboutSection.Element4'))}</li>
          <li>{parse(t('AboutSection.Element5'))}</li>
        </ul>
  
        <p className='docu-sign-link'><a href={parse(t('DocuSignLink'))}>{parse(t('DocuSignLinkTitle'))}</a></p>
        <hr/>
      </div>

    </div>
  );
};