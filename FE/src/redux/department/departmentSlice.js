import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import instance from '~/api/intance'

// Khởi tạo giá trị State cho departmentSlice
const initialState = {
  currentDepartment: [],
}

// Middle AsyncThunk để call api
export const fetchDepartmentDetailsApi = createAsyncThunk(
  'department/fetchDepartmentDetailsApi',
  async () => {
    const response = await instance.get("/Department/getall")
    return response.data
  }
)

export const fetchDepartmentAddApi = createAsyncThunk(
  "department/fetchDepartmentAddApi",
  async (data) => {
    const response = await instance.post("/Department/AddDepartment", data)

    return response.data
  }
)

export const fetchDepartmentEditApi = createAsyncThunk(
  "department/fetchDepartmentEditApi",
  async ({ id, data }) => {
    const response = await instance.put(`/Department/update/${id}`, data)
    return response.data
  }
)

export const fetchDepartmentDeleteApi = createAsyncThunk(
  "department/fetchDepartmentDeleteApi",
  async (id) => {
    const response = await instance.delete(`/Department/delete/${id}`)

    return response.data
  }
)

export const departmentSlice = createSlice({
  name: 'department',
  initialState,
  // Reducer nơi xử lý dữ liệu đồng bộ
  reducers: {
    addDepartment: (state, action) => {
      state.currentDepartment.push(action.payload)
    },

    deleteDepartment: (state, action) => {
      state.currentDepartment = state.currentDepartment.filter(item => item.departmentID != action.payload)
    }
  },

  // Nơi xử lý dữ liệu bất đồng bộ
  extraReducers: (builder) => {
    builder.addCase(fetchDepartmentDetailsApi.fulfilled, (state, action) => {
      state.currentDepartment = action.payload
    })

    builder.addCase(fetchDepartmentAddApi.fulfilled, (state, action) => {
      state.currentDepartment.push(action.payload)
    })

    builder.addCase(fetchDepartmentEditApi.fulfilled, (state, action) => {
      const index = state.currentDepartment.findIndex(item => item.departmentID == action.payload.departmentID)

      state.currentDepartment[index] = action.payload
    })
  }
})

// Action creators are generated for each case reducer function
export const { addDepartment, deleteDepartment } = departmentSlice.actions

export const departmentReducer = departmentSlice.reducer