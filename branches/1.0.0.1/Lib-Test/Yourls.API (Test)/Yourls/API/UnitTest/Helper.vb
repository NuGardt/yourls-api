' NuGardt YOURLS API
' Copyright (C) 2013 NuGardt Software
' http://www.nugardt.com
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports NuGardt.Yourls.API.Result
Imports System.Text

Namespace Yourls.API.UnitTest
  Public NotInheritable Class Helper(Of T As IYourlsBaseResult)

#Region "Function CheckResult"

    ''' <summary>
    ''' Checks the response and parsed object for accuracy.
    ''' </summary>
    ''' <param name="Description"></param>
    ''' <param name="Ex"></param>
    ''' <param name="Response"></param>
    ''' <remarks></remarks>
    Public Shared Function CheckResult(ByVal Description As String,
                                       <Out> ByRef Ex As Exception,
                                       ByVal Response As T,
                                       Optional ShowOnlyMismatches As Boolean = True,
                                       Optional TreatSoftFailAsFail As Boolean = False) As String
      Ex = Nothing
      Dim SB As New StringBuilder

      Call SB.AppendLine(Description)
      Call SB.AppendLine(New String("="c, Description.Length))

      Call SB.AppendLine("Class:")

      If (Response IsNot Nothing) Then
        Dim tResponse As IEnumerable
        tResponse = TryCast(Response, IEnumerable)

        If (tResponse IsNot Nothing) Then
          With tResponse.GetEnumerator()
            Call .Reset()

            Do While .MoveNext()
              Call SB.AppendLine(.Current.ToString)
            Loop
          End With
        Else
          Call SB.AppendLine(Response.ToString)
        End If

        Call SB.AppendLine("JSON Raw:")
        Call SB.AppendLine(Response.ResponseRaw)
        Call SB.AppendLine("")

        If (Not SanityCheck(SB, Response.ResponseRaw, Response, ShowOnlyMismatches, TreatSoftFailAsFail)) Then
          If Response.CacheExpires.HasValue Then
            Call SB.AppendLine("Result: FAIL (cached): Sanity check")
          Else
            Call SB.AppendLine("Result: FAIL: Sanity check")
          End If

          Ex = New Exception("Fail")
        Else
          If Response.CacheExpires.HasValue Then
            Call SB.AppendLine("Result: PASS (cached)")
          Else
            Call SB.AppendLine("Result: PASS")
          End If
        End If
      Else
        Call SB.AppendLine("Result: FAIL: No Response returned")
        Ex = New Exception("No Response returned.")
      End If

      Return SB.ToString
    End Function

    ''' <summary>
    ''' Compare the JSON response to the parsed data.
    ''' </summary>
    ''' <param name="SB"></param>
    ''' <param name="ResponseRaw">JSON response.</param>
    ''' <param name="Obj">Parsed object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SanityCheck(ByVal SB As StringBuilder,
                                        ByVal ResponseRaw As String,
                                        ByVal Obj As T,
                                        Optional ShowOnlyMismatches As Boolean = True,
                                        Optional TreatSoftFailAsFail As Boolean = False) As Boolean
      Dim ResponseReSerialized As String

      ResponseReSerialized = JsonConvert.SerializeObject(Obj, Formatting.None)

      Return CompareJson(SB, ResponseRaw, ResponseReSerialized, ShowOnlyMismatches, TreatSoftFailAsFail)
    End Function

#Region "Function CompareJson"

    ''' <summary>
    '''   Compare 2 JSON strings.
    ''' </summary>
    ''' <param name="SB">Write results to StringBuilder.</param>
    ''' <param name="A">JSON string A.</param>
    ''' <param name="B">JSON string B.</param>
    ''' <param name="ShowOnlyMismatches">
    '''   If <c>True</c> then only mismatches are show, otherwise matches and mismatches,
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CompareJson(ByVal SB As StringBuilder,
                                        ByVal A As String,
                                        ByVal B As String,
                                        Optional ShowOnlyMismatches As Boolean = True,
                                        Optional TreatSoftFailAsFail As Boolean = False) As Boolean

      If (A IsNot Nothing) AndAlso (B IsNot Nothing) Then
        Dim MismatchCount As Int32 = 0

        Try
          Dim ItemsA As JContainer = DirectCast(JsonConvert.DeserializeObject(A), JContainer)
          Dim ItemsB As JContainer = DirectCast(JsonConvert.DeserializeObject(B), JContainer)
          Dim Dict As New SortedDictionary(Of String, Object)
          Dim Key As String

          If ItemsA.GetType() <> ItemsB.GetType() Then
            Call SB.AppendLine("A and B are not comparable as they are not of the same type. Mismatch.")
            MismatchCount += 1
          Else
            If TypeOf ItemsA Is JArray Then
              Dim aA As JArray = DirectCast(ItemsA, JArray)
              Dim aB As JArray = DirectCast(ItemsB, JArray)

              If aA.Count <> aB.Count Then
                Call SB.AppendLine("A and B are not of the same array size. Mismatch.")
                MismatchCount += 1
              Else
                Dim dMax As Int32 = aA.Count - 1
                For i As Int32 = 0 To dMax
                  Call EnumerateJObject(i.ToString(), Dict, DirectCast(aA.Item(i), JObject), DirectCast(aB.Item(i), JObject))
                Next i
              End If
            Else
              Call EnumerateJObject("", Dict, DirectCast(ItemsA, JObject), DirectCast(ItemsB, JObject))
            End If

            With Dict.GetEnumerator()
              Do While .MoveNext()
                Key = .Current.Key

                If TypeOf .Current.Value Is JObject Then
                  With DirectCast(.Current.Value, JObject)
                    Call SB.AppendLine(String.Format("{0}: {1} <<< Mismatch", Key, .ToString()))
                    MismatchCount += 1
                  End With
                Else
                  With DirectCast(.Current.Value, CompareAandB)
                    If .IsEqual Then
                      If (Not ShowOnlyMismatches) Then Call SB.AppendLine(String.Format("{0}: {1} = {2}", Key, .A, .B))
                    Else
                      If .IsEqualString() AndAlso (Not TreatSoftFailAsFail) Then
                        Call SB.AppendLine(String.Format("{0}: {1} ({2}) <> {3} ({4}) <<< Soft Fail: Data Type mismatch", Key, .A, DirectCast(.A, JValue).Type.ToString(), .B, DirectCast(.B, JValue).Type.ToString()))
                      Else
                        Call SB.AppendLine(String.Format("{0}: {1} ({2}) <> {3} ({4}) <<< Fail: Data mismatch", Key, .A, DirectCast(.A, JValue).Type.ToString(), .B, DirectCast(.B, JValue).Type.ToString()))
                        MismatchCount += 1
                      End If
                    End If
                  End With
                End If
              Loop

              Call .Dispose()
            End With

            If (MismatchCount <> 0) Then Call SB.AppendLine("Mismatch count: " + MismatchCount.ToString())
          End If
        Catch iEx As Exception
          MismatchCount += 1
          Call SB.AppendLine(iEx.ToString())
        End Try

        Return (MismatchCount = 0)
      Else
        Return False
      End If
    End Function

    Private Shared Function EnumerateJObject(ByVal Obj As JObject) As String
      Dim dict As New SortedDictionary(Of String, String)
      Dim SB As New StringBuilder

      For Each o In Obj
        If TypeOf o.Value Is JObject Then
          Call dict.Add(o.Key, EnumerateJObject(DirectCast(o.Value, JObject)))
        Else
          Call dict.Add(o.Key, o.Value.ToString())
        End If
      Next o

      With dict.GetEnumerator()
        Do While .MoveNext()
          Call SB.AppendLine(.Current.Key + ": " + .Current.Value.ToString())
        Loop

        Call .Dispose()
      End With

      Return SB.ToString()
    End Function

    ''' <summary>
    '''   Class for storing results of the compare.
    ''' </summary>
    ''' <remarks></remarks>
    Private NotInheritable Class CompareAandB
      Public ReadOnly A As Object
      Public B As Object

      Public Sub New(ByVal A As Object,
                     ByVal B As Object)
        Me.A = A
        Me.B = B
      End Sub

      Public Function IsEqual() As Boolean
        If A IsNot Nothing Then
          'A = Something

          If B IsNot Nothing Then
            'B = Something

            Return A.Equals(B)
          Else
            'B = Nothing
            Return False
          End If
        ElseIf B IsNot Nothing Then
          'A = Nothing, B = Something
          Return False
        Else
          'A = Nothing, B = Nothing
          Return True
        End If
      End Function

      Public Function IsEqualString() As Boolean
        If A IsNot Nothing Then
          'A = Something

          If B IsNot Nothing Then
            'B = Something

            Return String.Equals(A.ToString(), B.ToString(), StringComparison.InvariantCultureIgnoreCase)
          Else
            'B = Nothing
            Return False
          End If
        ElseIf B IsNot Nothing Then
          'A = Nothing, B = Something
          Return False
        Else
          'A = Nothing, B = Nothing
          Return True
        End If
      End Function
    End Class

    Private Shared Sub EnumerateJObject(ByVal KeyPrefix As String,
                                        ByVal Dict As IDictionary(Of String, Object),
                                        ByVal A As JObject,
                                        ByVal B As JObject)
      Dim tObj As Object = Nothing

      For Each o In A
        If Not Dict.ContainsKey(KeyPrefix + o.Key) Then
          If TypeOf o.Value Is JObject Then
            Call Dict.Add(KeyPrefix + o.Key, DirectCast(o.Value, JObject))
          ElseIf TypeOf o.Value Is JArray Then
            Call Dict.Add(KeyPrefix + o.Key, DirectCast(o.Value, JArray))
          Else
            Call Dict.Add(KeyPrefix + o.Key, New CompareAandB(o.Value, Nothing))
          End If
        End If
      Next o

      For Each o In B
        If TypeOf o.Value Is JObject Then
          If Dict.ContainsKey(KeyPrefix + o.Key) Then
            Call Dict.TryGetValue(KeyPrefix + o.Key, tObj)
            Call Dict.Remove(KeyPrefix + o.Key)

            Call EnumerateJObject(KeyPrefix + o.Key, Dict, DirectCast(tObj, JObject), DirectCast(o.Value, JObject))
          Else
            Call Dict.Add(KeyPrefix + o.Key, EnumerateJObject(DirectCast(o.Value, JObject)))
          End If
        ElseIf TypeOf o.Value Is JArray Then
          If Dict.ContainsKey(KeyPrefix + o.Key) Then
            Call Dict.TryGetValue(KeyPrefix + o.Key, tObj)
            Call Dict.Remove(KeyPrefix + o.Key)

            Dim jA As JArray = DirectCast(tObj, JArray)
            Dim jB As JArray = DirectCast(o.Value, JArray)

            If jA.Count = jB.Count Then
              Dim dMax As Int32 = jA.Count - 1
              For i As Int32 = 0 To dMax
                Call EnumerateJObject(KeyPrefix + o.Key + i.ToString(), Dict, DirectCast(jA.Item(i), JObject), DirectCast(jB.Item(i), JObject))
              Next i
            Else
              Call Dict.Add(KeyPrefix + o.Key, "<<< Mismatch!!!")
            End If
          Else
            Call Dict.Add(KeyPrefix + o.Key, EnumerateJObject(DirectCast(o.Value, JObject)))
          End If
        Else
          If Dict.ContainsKey(KeyPrefix + o.Key) Then
            Call Dict.TryGetValue(KeyPrefix + o.Key, tObj)
            Call Dict.Remove(KeyPrefix + o.Key)

            With DirectCast(tObj, CompareAandB)
              .B = o.Value
            End With

            Call Dict.Add(KeyPrefix + o.Key, tObj)
          Else
            Call Dict.Add(KeyPrefix + o.Key, New CompareAandB(Nothing, o.Value))
          End If
        End If
      Next o
    End Sub

#End Region

#End Region
  End Class
End Namespace