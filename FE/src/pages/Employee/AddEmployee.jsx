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
import { useDispatch, useSelector } from "react-redux";

const AddEmployee = () => {
  const navigate = useNavigate();
  const departments = useSelector(
    (state) => state.department.currentDepartment
  );
  const positions = useSelector((state) => state.position.currentPosition);
  const dispatch = useDispatch();

  const optionsGender = [
    { value: "nam", label: "Nam" },
    { value: "Nữ", label: "Nữ" },
    { value: "Khác", label: "Khác" },
  ];

  const optionsDepartment = departments.map((department) => ({
    value: department.departmentID,
    label: department.departmentName,
  }));

  const optionsPosition = positions.map((position) => ({
    value: position.positionID,
    label: position.positionName,
  }));

  return (
    <>
      <div className="employee__add contain">
        <Header
          title="Add New Employee"
          subTitle="All Employee > Add New Employee"
        />

        <div className="employee__form">
          <Flex align="center" gap={5} className="employee__head">
            <UserOutlined style={{ color: "var(--color-purple)" }} />
            <span>Thông tin cá nhân</span>
          </Flex>
          <Divider />

          <Form
            initialValues={{
              hire_date: dayjs("04/02/2025", "DD/MM/YYYY"),
            }}
          >
            <Row align="center" gap={20} gutter={[20, 20]}>
              <Col sm={12}>
                <Form.Item
                  name="employeeID"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập mã nhân viên!",
                    },
                  ]}
                >
                  <Input placeholder="Mã nhân viên" />
                </Form.Item>
              </Col>

              <Col sm={12}>
                <Form.Item
                  name="fullName"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập tên nhân viên!",
                    },
                  ]}
                >
                  <Input placeholder="Tên nhân viên" />
                </Form.Item>
              </Col>
            </Row>

            <Row align="center" gap={20} gutter={[20, 20]}>
              <Col sm={12}>
                <Form.Item
                  name="dateOfBirth"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng nhập ngày sinh!",
                    },
                  ]}
                >
                  <DatePicker
                    format="DD/MM/YYYY"
                    style={{ width: "100%" }}
                    placeholder="Ngày sinh"
                  />
                </Form.Item>
              </Col>

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
            </Row>

            <Row align="center" gap={20} gutter={[20, 20]}>
              <Col sm={12}>
                <Form.Item
                  name="phoneNumber"
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
                  name="application_date"
                  rules={[
                    {
                      required: true,
                      message: "Vui lòng chọn",
                    },
                  ]}
                >
                  <DatePicker format="DD/MM/YYYY" style={{ width: "100%" }} />
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
                  <DatePicker format="DD/MM/YYYY" style={{ width: "100%" }} />
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

export default AddEmployee;
