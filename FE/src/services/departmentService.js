import instance from "~/api/intance"

const getDepartment = async () => {
  const response = await instance.get("/Department/getall")
  
  return response.data
}

const addDepartment = async (data) => {
  return await instance.post("/Department/AddDepartment", data);
}

const departmentService = {
  getDepartment,
  addDepartment
}

export default departmentService