import { signingReadyEvent, sessionEndEvent, signingResult, loanTypes } from '../constants';

const { personal } = loanTypes;

export const handleDocuSignLoad = async (data, updateStatus, navigate, ind) => {
  const { instanceToken, integrationKey, url, id, formId } = data || {};
  const isFocusedView = ind === personal;
  const { loadDocuSign } = window.DocuSign;
  const docusign = await loadDocuSign(integrationKey);
  const token = instanceToken !== undefined ? instanceToken.replace(/&#x2B;/g, '+') : instanceToken;
  const webFormOptions = {
    instanceToken: token,
    hideProgressBar: false,
    styles: {
      fontColor: 'black',
    },
    iframeStyles: {
      minHeight: '1100px',
    },
    autoResizeHeight: true,
    tracking: {
      'tracking-field': 'tracking-value',
    },
    hidden: {
      'hidden-field': 'hidden-value',
    },
  };
  const webFormWidget = docusign.webforms({
    url,
    options: webFormOptions,
  });

  webFormWidget.on(signingReadyEvent, async (event) => {
    const { type } = event;
    if (!isFocusedView && type === signingReadyEvent) {
      const { redirectUris } = await updateStatus({
        id,
        formId,
        isFocusedView
      }).unwrap();
      window.location.assign(redirectUris[0]);
    }
  });

  webFormWidget.on(sessionEndEvent, async (event) => {
    const { sessionEndType: type } = event;
    if (isFocusedView && type === signingResult) {
      await updateStatus({
        id,
        formId,
        isFocusedView
      });
      navigate('/sign/completed?event=signing_focused_view_complete', { relative: 'route' });
    }
  });

  webFormWidget.mount('#docusign');
};
