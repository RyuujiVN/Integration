import { ConfigProvider, theme } from "antd";
import AllRoute from "./components/AllRoute/AllRoute";
import "./App.css";
import { ThemeContext } from "~/context/themeContext";
import { useContext } from "react";
import "@ant-design/v5-patch-for-react-19";

function App() {
  const { myTheme } = useContext(ThemeContext);

  return (
    <>
      <ConfigProvider
        theme={{
          token: {
            colorPrimary: "#7152F3",
            fontSize: 16,
            fontFamily: "Lexend, sans-serif",
          },
          components: {
            Button: {
              colorPrimary: "#7152F3",
              algorithm: true,
            },
            Tag: {
              colorSuccess: "#3FC28A",
              colorSuccessBg: "rgba(63, 194, 138, 0.10)",
            },
            Segmented: {
              itemSelectedBg: "var(--color-purple)",
              itemSelectedColor: "#fff",
            },
          },
          algorithm:
            myTheme === "light" ? theme.defaultAlgorithm : theme.darkAlgorithm,
        }}
      >
        <AllRoute />
      </ConfigProvider>
    </>
  );
}

export default App;
