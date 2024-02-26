const loanTypes = {
  personal: 0,
  auto: 1,
  sailboat: 2,
};

const sessionEndEvent = 'sessionEnd';
const signingReadyEvent = 'signingReady';
const sentStatus = 'sent';
const signingCompletedEvent = 'signing_complete';
const signingResult = 'signingResult';
const signingFocusedViewCompletedEvent = 'signing_focused_view_complete';

export {
  loanTypes,
  signingReadyEvent,
  signingCompletedEvent,
  sessionEndEvent,
  sentStatus,
  signingFocusedViewCompletedEvent,
  signingResult
};
