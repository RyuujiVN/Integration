import { ConfigProvider } from "antd";
import AllRoute from "./components/AllRoute/AllRoute";

function App() {
  return (
    <>
      <ConfigProvider
        theme={{
          token: {
            colorPrimary: "#7152F3",
          },
        }}
      >
        <AllRoute />
      </ConfigProvider>
    </>
  );
}

export default App;
