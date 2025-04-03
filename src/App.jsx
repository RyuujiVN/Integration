import { ConfigProvider } from "antd";
import AllRoute from "./components/AllRoute/AllRoute";
import "./App.css";

function App() {
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
              colorSuccessBg: "rgba(63, 194, 138, 0.10)"
              
            },
          },
        }}
      >
        <AllRoute />
      </ConfigProvider>
    </>
  );
}

export default App;
