import { Button, Form, Input, Modal } from "antd";

export const EditDepartment = (props) => {
  const { open, setOpen } = props;
  const [form] = Form.useForm();

  const handleEdit = (value) => {
    console.log(value);
  };

  return (
    <>
      <Modal
        title={<h4 className="modal__title">Chỉnh sửa phòng ban</h4>}
        open={open}
        onCancel={() => setOpen(false)}
        footer={[
          <Button variant="outlined" onClick={() => setOpen(false)}>
            Huỷ
          </Button>,
          <Button type="primary" onClick={() => form.submit()}>
            Chỉnh sửa
          </Button>,
        ]}
      >
        <Form form={form} layout="vertical" onFinish={handleEdit}>
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
            <Input.TextArea maxLength={255} showCount />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};
