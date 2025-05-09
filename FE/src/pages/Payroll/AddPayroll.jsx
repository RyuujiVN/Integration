import { Button, Form, Input, InputNumber, Modal, Select } from "antd";
import React from "react";
import { formatCurrency, parseCurrency } from "~/utils/format";

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
            name="salaryID"
            label="Mã lương"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập mã lương",
              },
            ]}
          >
            <InputNumber
              className="input"
              min={0}
              formatter={formatCurrency}
              parser={parseCurrency}
            />
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
            name="salaryMonth"
            label="Nhập lương tháng:"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập lương tháng",
              },
            ]}
          >
            <InputNumber
              className="input"
              min={0}
              formatter={formatCurrency}
              parser={parseCurrency}
            />
          </Form.Item>

          <Form.Item
            name="baseSalary"
            label="Nhập lương cơ bản:"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập lương cơ bản",
              },
            ]}
          >
            <InputNumber
              className="input"
              min={0}
              formatter={formatCurrency}
              parser={parseCurrency}
            />
          </Form.Item>

          <Form.Item
            name="bonus"
            label="Nhập thưởng"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập thưởng",
              },
            ]}
          >
            <InputNumber
              className="input"
              min={0}
              formatter={formatCurrency}
              parser={parseCurrency}
            />
          </Form.Item>

          <Form.Item
            name="deductions"
            label="Nhập khấu trừ:"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập khấu trừ",
              },
            ]}
          >
            <InputNumber
              className="input"
              min={0}
              formatter={formatCurrency}
              parser={parseCurrency}
            />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};

export default AddPayroll;
