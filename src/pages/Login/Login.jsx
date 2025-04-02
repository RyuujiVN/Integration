import { Button, Checkbox, Flex, Form, Input } from "antd";
import Background from "~/assets/images/Background.jpg";
import Logo from "~/assets/images/Logo.png";
import "./Login.css";
import { Link } from "react-router-dom";

const Login = () => {
  return (
    <>
      <div className="login">
        <div className="login__container">
          <div className="login__container--left">
            <img src={Background} alt="Background" />
          </div>
          <div className="login__container--right">
            <div className="login__form">
              <Form>
                <div className="login__img">
                  <img src={Logo} alt="Logo" style={{ height: "100px" }} />
                </div>
                <h2 className="login__title">Login</h2>

                <Form.Item
                  name="email"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập email!",
                    },

                    {
                      pattern: /^\S+@\S+.\S+$/,
                      message: "Vui lòng nhập email đúng định dạng!",
                    },
                  ]}
                >
                  <Input placeholder="Email..." />
                </Form.Item>

                <Form.Item
                  name="password"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập mật khẩu!",
                    },
                  ]}
                >
                  <Input.Password placeholder="Mật khẩu..." />
                </Form.Item>

                <Form.Item>
                  <Flex justify="space-between" align="center">
                    <Form.Item name="rememeber" valuePropName="checked" noStyle>
                      <Checkbox>Nhớ đăng nhập</Checkbox>
                    </Form.Item>
                    <Link
                      to="/forgot-password"
                      className="login__forgot-password"
                    >
                      Quên mật khẩu
                    </Link>
                  </Flex>
                </Form.Item>

                <Form.Item>
                  <Button block type="primary">
                    Đăng nhập
                  </Button>
                </Form.Item>
              </Form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Login;
