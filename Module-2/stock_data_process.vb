Sub stock_data_process()

'Suvrangshu Ghosh

'Get all worksheet and put in loop to calculate details

'-----------------------------------

   For Each ws In Worksheets

   'Counting the number of rows in the sheet
   '----------------------------------------
   
      lastrow = ws.Cells(Rows.Count, 1).End(xlUp).Row
      
   'Writing the Header for the derived columns
   '------------------------------------------
   
      ws.Range("I1, P1").Value = "Ticker"
      ws.Cells(1, 10).Value = "Yearly Change"
      ws.Cells(1, 11).Value = "Percent Change"
      ws.Cells(1, 12).Value = "Total Stock Volume"
      ws.Cells(1, 17).Value = "Value"
      ws.Cells(2, 15).Value = "Greatest % Increase"
      ws.Cells(3, 15).Value = "Greatest % Decrease"
      ws.Cells(4, 15).Value = "Greatest Total Volume"
   
   'Formatting the output columns
   '-----------------------------
   
      ws.Range("Q2:Q3").NumberFormat = "#,##0.00%"
      ws.Range("Q4").NumberFormat = "##############0"
   
      
   'Initialization of variables
   '---------------------------
   
      sum_row = 2                                 'To determine where to write the first total
      
      total_volume = 0                            'Variable to hold total volume for a stock
      
      opening_value = ws.Cells(2, 3).Value        'Opening value of the first stock
      
      ws.Cells(2, 17).Value = 0                   'Greatest % increase
      ws.Cells(3, 17).Value = 0                   'Greatest % decrease
      ws.Cells(4, 17).Value = 0                   'Greatest volume
            
   'Looping through all the rows in the sheet
   '-----------------------------------------
   
      For i = 2 To lastrow
         
      'If next row has different stock name, i.e, completing scanning of one stock
      '---------------------------------------------------------------------------
      
         If ws.Cells(i, 1).Value <> ws.Cells(i + 1, 1) Then
      
         'Write the name and total value of stock in the total columns
         '------------------------------------------------------------
            ws.Cells(sum_row, 9).Value = ws.Cells(i, 1)
            ws.Cells(sum_row, 12).Value = total_volume + ws.Cells(i, 7)
            
         'Calculate and write the change in yearly value, format cell
         '-----------------------------------------------------------
            value_change = ws.Cells(i, 6).Value - opening_value
            ws.Cells(sum_row, 10).Value = value_change
            
            If value_change > 0 Then
               ws.Cells(sum_row, 10).Interior.ColorIndex = 4
            ElseIf value_change < 0 Then
               ws.Cells(sum_row, 10).Interior.ColorIndex = 3
            End If
            
         'Calculate and write the value change percentage, format cell
         '------------------------------------------------------------
            If opening_value <> 0 Then
               value_change_per = value_change / opening_value
            Else
               value_change_per = 0
            End If
            
            ws.Cells(sum_row, 11).Value = value_change_per
            
            ws.Cells(sum_row, 11).NumberFormat = "#,##0.00%"
                        
         'Greatest Percent Increase
         '-------------------------
            If value_change_per > ws.Cells(2, 17).Value Then
               ws.Cells(2, 17).Value = value_change_per
               ws.Cells(2, 16).Value = ws.Cells(i, 1).Value
            End If
            
         'Greatest Percent Decrease
         '-------------------------
            If value_change_per < ws.Cells(3, 17).Value Then
               ws.Cells(3, 17).Value = value_change_per
               ws.Cells(3, 16).Value = ws.Cells(i, 1).Value
            End If
            
         'Greatest Stock Volume
         '---------------------
            If ws.Cells(sum_row, 12).Value > ws.Cells(4, 17).Value Then
               ws.Cells(4, 17).Value = ws.Cells(sum_row, 12).Value
               ws.Cells(4, 16).Value = ws.Cells(sum_row, 9).Value
            End If
            
         'Updating parameters after one stock search is over
         '--------------------------------------------------
         
            sum_row = sum_row + 1
         
            total_volume = 0
            
            opening_value = ws.Cells(i + 1, 3).Value
                     
         Else
         
         'Updating the total volume
         '-------------------------
            total_volume = total_volume + ws.Cells(i, 7).Value
         
         End If
      
      Next i
      
   'Autofit the added columns
   '-------------------------
      ws.Range("I1:L1").Columns.AutoFit
      ws.Range("O4,Q4").Columns.AutoFit
      ws.Range("P1").Columns.AutoFit
   
   Next ws

End Sub
