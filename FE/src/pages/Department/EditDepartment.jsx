import { Button, Form, Input, message, Modal } from "antd";
import { useDispatch } from "react-redux";
import messageUtil from "~/utils/message";
import { fetchDepartmentEditApi } from "~/redux/department/departmentSlice";

export const EditDepartment = (props) => {
  const { open, setOpen, department } = props;
  const [messageApi, messageContextHolder] = message.useMessage();

  const dispatch = useDispatch();
  const [form] = Form.useForm();

  const handleEdit = async (value) => {
    await dispatch(
      fetchDepartmentEditApi({
        id: department.departmentID,
        data: value,
      })
    );

    messageUtil(messageApi, "success", "Chỉnh sửa thành công!");
  };

  return (
    <>
      {messageContextHolder}
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
        <Form
          form={form}
          layout="vertical"
          onFinish={handleEdit}
          initialValues={department}
        >
          <Form.Item
            name="departmentName"
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
        </Form>
      </Modal>
    </>
  );
};
