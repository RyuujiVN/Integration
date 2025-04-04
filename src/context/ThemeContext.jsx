/* eslint-disable react-refresh/only-export-components */
import React, { createContext, useState } from "react";

export const ThemeContext = createContext();

export const ThemeProvider = ({ children }) => {
  const [myTheme, setMyTheme] = useState(() => localStorage.getItem("theme"));

  const toggleTheme = (theme) => {
    setMyTheme(theme);
    localStorage.setItem("theme", theme);
  };

  return (
    <>
      <ThemeContext.Provider value={{ myTheme, toggleTheme }}>
        {children}
      </ThemeContext.Provider>
    </>
  );
};
