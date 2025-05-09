import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import instance from '~/api/intance'

const initialState = {
  currentPayroll: [],
}

export const fetchPayrollAllApi = createAsyncThunk(
  "payroll/fetchPayrollAllApi",
  async () => {
    const response = await instance.get("/Salary/GetAllSalaries")

    return response.data
  }
)

export const payrollSlice = createSlice({
  name: 'payroll',
  initialState,
  reducers: {

  },

  extraReducers: (builder) => {
    builder.addCase(fetchPayrollAllApi.fulfilled, (state, action) => {
      state.currentPayroll = action.payload
    })
  }
})

// Action creators are generated for each case reducer function
export const { increment, decrement, incrementByAmount } = payrollSlice.actions

export const payrollReducer = payrollSlice.reducer