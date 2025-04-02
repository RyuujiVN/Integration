import { Layout } from "antd";
import { Content } from "antd/es/layout/layout";
import { Outlet } from "react-router-dom";
import Sidebar from "~/components/Sidebar/Sidebar";

const DefaultLayout = () => {
  return (
    <>
      <Layout
        style={{
          minHeight: "100vh",
        }}
      >
        <Sidebar />
        <Content style={{ background: "#F9FBFD" }}>
          <Outlet />
        </Content>
      </Layout>
    </>
  );
};

export default DefaultLayout;
