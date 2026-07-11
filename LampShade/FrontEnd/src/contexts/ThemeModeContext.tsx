import { createContext, useContext, useEffect, useState, type ReactNode } from 'react';

type ThemeMode = 'light' | 'dark';

interface ThemeModeContextValue {
  mode: ThemeMode;
  isDark: boolean;
  toggleTheme: () => void;
}

const ThemeModeContext = createContext<ThemeModeContextValue | undefined>(undefined);

const getInitialMode = (): ThemeMode => localStorage.getItem('theme-mode') === 'dark' ? 'dark' : 'light';

export const ThemeModeProvider = ({ children }: { children: ReactNode }) => {
  const [mode, setMode] = useState<ThemeMode>(getInitialMode);

  useEffect(() => {
    localStorage.setItem('theme-mode', mode);
    document.documentElement.dataset.theme = mode;
  }, [mode]);

  const toggleTheme = () => setMode(current => current === 'light' ? 'dark' : 'light');

  return <ThemeModeContext.Provider value={{ mode, isDark: mode === 'dark', toggleTheme }}>
    {children}
  </ThemeModeContext.Provider>;
};

export const useThemeMode = () => {
  const context = useContext(ThemeModeContext);
  if (!context) throw new Error('useThemeMode must be used within ThemeModeProvider');
  return context;
};