import React from "react";
import Header from "~/components/Header/Header";
import { UserOutlined } from "@ant-design/icons";
import {
  Button,
  Col,
  DatePicker,
  Divider,
  Flex,
  Form,
  InputNumber,
  Row,
  Select,
} from "antd";
import Input from "antd/es/input/Input";
import dayjs from "dayjs";
import { useNavigate } from "react-router-dom";

const EditEmployee = () => {
  const navigate = useNavigate();

  return (
    <>
      <div className="employee__eidt contain">
        <Header
          title="Chỉnh sửa nhân viên"
          subTitle="Danh sách nhân viên > Chỉnh sửa nhân viên"
        />

        <div className="employee__form">
          <Flex align="center" gap={5} className="employee__head">
            <UserOutlined style={{ color: "var(--color-purple)" }} />
            <span>Thông tin cá nhân</span>
          </Flex>
          <Divider />

          <Form>
            <Row align="center" gap={20} gutter={[20, 20]}>
              <Col sm={12}>
                <Form.Item
                  name="first_name"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập họ!",
                    },
                  ]}
                >
                  <Input placeholder="Họ" />
                </Form.Item>
              </Col>

              <Col sm={12}>
                <Form.Item
                  name="last_name"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập tên!",
                    },
                  ]}
                >
                  <Input placeholder="Tên" />
                </Form.Item>
              </Col>
            </Row>

            <Row align="center" gap={20} gutter={[20, 20]}>
              <Col sm={12}>
                <Form.Item
                  name="phone"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập số điện thoại!",
                    },
                    {
                      pattern: /^\d+$/,
                      message: "Số điện thoại không được chứa chữ",
                    },
                  ]}
                >
                  <Input placeholder="Số điện thoại" />
                </Form.Item>
              </Col>

              <Col sm={12}>
                <Form.Item
                  name="email"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập email!",
                    },
                    {
                      pattern: /^\S+@\S+.\S+$/,
                      message: "Vui lòng nhập đúng định dạng email",
                    },
                  ]}
                >
                  <Input placeholder="Tên" />
                </Form.Item>
              </Col>
            </Row>

            <Row align="center" gap={20} gutter={[20, 20]}>
              <Col sm={12}>
                <Form.Item
                  name="job_id"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng chọn",
                    },
                  ]}
                >
                  <Select placeholder="Chọn công việc" />
                </Form.Item>
              </Col>

              <Col sm={12}>
                <Form.Item
                  name="department_id"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng chọn",
                    },
                  ]}
                >
                  <Select placeholder="Chọn vị trí" />
                </Form.Item>
              </Col>
            </Row>

            <Row align="center" gap={20} gutter={[20, 20]}>
              <Col sm={12}>
                <Form.Item
                  name="hire_date"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng chọn",
                    },
                  ]}
                >
                  <DatePicker
                    defaultValue={dayjs("04/02/2025", "DD/MM/YYYY")}
                    format="DD/MM/YYYY"
                    style={{ width: "100%" }}
                  />
                </Form.Item>
              </Col>

              <Col sm={12}>
                <Form.Item
                  name="status"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng chọn",
                    },
                  ]}
                >
                  <Select placeholder="Trạng thái" />
                </Form.Item>
              </Col>
            </Row>

            <Row align="center" gap={20} gutter={[20, 20]}>
              <Col sm={8}>
                <Form.Item
                  name="hire_date"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập ngày",
                    },
                  ]}
                >
                  <DatePicker
                    defaultValue={dayjs("04/02/2025", "DD/MM/YYYY")}
                    format="DD/MM/YYYY"
                    style={{ width: "100%" }}
                  />
                </Form.Item>
              </Col>

              <Col sm={8}>
                <Form.Item
                  name="salary"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập lương!",
                    },
                  ]}
                >
                  <InputNumber
                    min={0}
                    placeholder="Lương"
                    style={{ width: "100%" }}
                  />
                </Form.Item>
              </Col>

              <Col sm={8}>
                <Form.Item
                  name="status"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng chọn",
                    },
                  ]}
                >
                  <Select placeholder="Trạng thái" />
                </Form.Item>
              </Col>
            </Row>

            <Form.Item className="employee__form--action">
              <Button
                variant="outlined"
                onClick={() => navigate(-1)}
                size="large"
                style={{ margin: 20 }}
              >
                Quay lại
              </Button>
              <Button type="primary" htmlType="submit" size="large">
                Thêm
              </Button>
            </Form.Item>
          </Form>
        </div>
      </div>
    </>
  );
};

export default EditEmployee;
