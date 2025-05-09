import { Modal } from "antd";
import React from "react";

const DetailDepartment = (props) => {
  const { open, setOpen, department } = props;

  return (
    <>
      <Modal
        title={<h4 className="modal__title">Chi tiết phòng ban</h4>}
        open={open}
        onCancel={() => setOpen(false)}
        footer={null}
      >
        <p>
          <strong>Mã phòng ban:</strong> {department?.departmentID}
        </p>
        <p>
          <strong>Tên phòng ban:</strong> {department?.departmentName}
        </p>
      </Modal>
    </>
  );
};

export default DetailDepartment;
