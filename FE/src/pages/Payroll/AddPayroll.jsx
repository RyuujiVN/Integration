import { Button, Form, Input, InputNumber, Modal, Select } from "antd";
import React from "react";

const AddPayroll = (props) => {
  const { open, setOpen } = props;
  const [form] = Form.useForm();
  const handleAdd = (value) => {
    console.log(value);
  };

  return (
    <>
      <Modal
        title={<h4 className="modal__title">Thêm bảng lương</h4>}
        open={open}
        onCancel={() => setOpen(false)}
        footer={[
          <Button key="cancel" onClick={() => setOpen(false)}>
            Huỷ
          </Button>,
          <Button
            key="submit"
            type="primary"
            htmlType="submit"
            onClick={() => form.submit()}
          >
            Thêm bảng lương
          </Button>,
        ]}
      >
        <Form form={form} onFinish={handleAdd} layout="vertical">
          <Form.Item
            name="department_id"
            label="Chọn phòng ban:"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập chọn phòng ban",
              },
            ]}
          >
            <Select />
          </Form.Item>

          <Form.Item
            name="job_id"
            label="Chọn job:"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập chọn job",
              },
            ]}
          >
            <Select />
          </Form.Item>

          <Form.Item
            name="bonus"
            label="Nhập thưởng:"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập thưởng",
              },
            ]}
          >
            <InputNumber className="input" min={0} />
          </Form.Item>

          <Form.Item
            name="salary_per_month"
            label="Nhập lương tháng:"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập lương tháng",
              },
            ]}
          >
            <InputNumber className="input" min={0} />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};

export default AddPayroll;
