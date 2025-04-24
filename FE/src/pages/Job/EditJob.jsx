import { Button, Form, Input, InputNumber, Modal, Select } from "antd";
import React from "react";

const EditJob = (props) => {
  const { open, setOpen } = props;
  const [form] = Form.useForm();
  const handleEdit = (value) => {
    console.log(value);
  };

  return (
    <>
      <Modal
        title={<h4 className="modal__title">Chỉnh sửa vị trí</h4>}
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
            Chỉnh sửa
          </Button>,
        ]}
      >
        <Form form={form} onFinish={handleEdit} layout="vertical">
          <Form.Item
            name="department_id"
            label="Tên phòng ban"
            rules={[
              {
                required: true,
                message: "Vui lòng chọn tên phòng ban",
              },
            ]}
          >
            <Select />
          </Form.Item>

          <Form.Item name="name" label="Tên vị trí" rules={[
            {
              required: true,
              message: "Vui lòng nhập tên vị trí"
            }
          ]}>
            <Input />
          </Form.Item>

          <Form.Item name="min_salary" label="Lương tối thiểu">
            <InputNumber min={0} className="input" defaultValue={0}/>
          </Form.Item>

          <Form.Item name="max_salary" label="Lương tối thiểu">
            <InputNumber min={0} className="input" defaultValue={0}/>
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};

export default EditJob;
