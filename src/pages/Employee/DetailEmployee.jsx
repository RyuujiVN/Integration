import { Modal } from "antd";

// { title: "Employee Name", dataIndex: "name" },
// { title: "Hire Date", dataIndex: "hireDate" },
// { title: "Department", dataIndex: "department" },
// { title: "Jobs", dataIndex: "job" },
// { title: "Salary", dataIndex: "salary" },
// { title: "Status", dataIndex: "status" },
const DetailEmployee = (props) => {
  const { open, setOpen } = props;

  return (
    <>
      <Modal
        title={<h3>Thông tin chi tiết</h3>}
        open={open}
        footer={null}
        onCancel={() => setOpen(false)}
      >
        <p><strong>Tên:</strong></p>
        <p><strong>Ngày vào làm:</strong></p>
        <p><strong>Phòng ban:</strong></p>
      </Modal>
    </>
  );
};

export default DetailEmployee;
