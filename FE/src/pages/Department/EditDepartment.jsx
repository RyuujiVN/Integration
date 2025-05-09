import { Button, Form, Input, Modal } from "antd";
import { useDispatch } from "react-redux";
import { fetchDepartmentEditApi } from "~/redux/department/departmentSlice";
import { toast } from "react-toastify";

export const EditDepartment = (props) => {
  const { open, setOpen, department } = props;

  const dispatch = useDispatch();
  const [form] = Form.useForm();

  const handleEdit = (value) => {
    toast.promise(
      dispatch(
        fetchDepartmentEditApi({
          id: department.departmentID,
          data: value,
        })
      ),
      {
        pending: "Đang chỉnh sửa...",
      }
    );
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
