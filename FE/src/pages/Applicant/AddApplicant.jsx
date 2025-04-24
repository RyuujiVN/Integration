import { Button, Form, Input, Modal } from "antd";
import React from "react";

const AddApplicant = (props) => {
  const { open, setOpen } = props;
  const [form] = Form.useForm();
  const handleAdd = (value) => {
    console.log(value);
  };

  return (
    <>
      <Modal
        title={<h4 className="modal__title">Thêm phòng ban</h4>}
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
            Thêm phòng ban
          </Button>,
        ]}
      >
        <Form form={form} onFinish={handleAdd} layout="vertical">
          <Form.Item
            name="name"
            label="Tên phòng ban"
            rules={[
              {
                required: true,
                message: "Vui lòng nhập tên phòng ban",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item name="description" label="Mô tả">
            <Input.TextArea showCount maxLength={255} />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};

export default AddApplicant;
