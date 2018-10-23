import React from "react";
import ReactDOM from "react-dom";

export const AlchemyContext = React.createContext({
    // theme: themes.dark,
    toggleLoading: () => {},
    toggleScanning: () => {},
    toggleWaiting: () => {},
  });