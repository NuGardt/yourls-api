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
Imports NuGardt.UnitTest
Imports NuGardt.Yourls.API.Result

Namespace Yourls.API.UnitTest.CreateShortUrl
  Public Class CreateShortUrlUnitTest
    Implements IUnitTestCase

    Private OnCompletion As AsyncCallback
    Private m_Result As String

    Private Service As YourlsService
    Private Ex As Exception

    Public Sub Initialize() Implements IUnitTestCase.Initialize
      Me.m_Result = Nothing

      Me.Service = New YourlsService(My.Resources.ApiUrl)
    End Sub

    Public Sub Start(ByVal OnCompletion As AsyncCallback,
                     Optional Report As IUnitTestCase.procReport = Nothing) Implements IUnitTestCase.Start
      Me.OnCompletion = OnCompletion

      If (Me.Ex Is Nothing) Then
        Dim Response As YourlsCreateShortUrlResult = Nothing
        Dim MyAuth As IYourlsAuthentication

        'Username/Password
        MyAuth = New YourlsAuthentication(My.Resources.Username, My.Resources.Password)

        'Signature only
        'MyAuth = New YourlsAuthentication(My.Resources.Signature)

        'Signature with Timestamp (Temporary short URLs)
        'MyAuth = New YourlsAuthentication(My.Resources.Signature, DateTime.Now)

        Ex = Service.CreateShortUrl(LongUrl := "http://www.nugardt.com", Authentication := MyAuth, Result := Response, Keyword := "", Title := "")

        If (Ex Is Nothing) Then
          If Response.HasError Then
            Me.Ex = Response.ToException()
          Else
            Me.m_Result = Helper(Of YourlsCreateShortUrlResult).CheckResult("CreateShortUrl", Me.Ex, Response, TreatSoftFailAsFail := False)
          End If
        End If
      End If

      Call Me.OnCompletion.Invoke(Nothing)
    End Sub

    Public Function Abort() As Boolean Implements IUnitTestCase.Abort
      Return False
    End Function

    Public Sub [End](Optional Result As IAsyncResult = Nothing) Implements IUnitTestCase.[End]
      '-
    End Sub

    Public Sub Dispose() Implements IUnitTestCase.Dispose
      '-
    End Sub

    Public ReadOnly Property Successfull As Boolean Implements IUnitTestCase.Successfull
      Get
        Return (Me.Ex Is Nothing)
      End Get
    End Property

    Public ReadOnly Property IsRunning As Boolean Implements IUnitTestCase.IsRunning
      Get
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property Name As String Implements IUnitTestCase.Name
      Get
        Return "YOURLS API: CreateShortUrl"
      End Get
    End Property

    Public ReadOnly Property Result As String Implements IUnitTestCase.Result
      Get
        If (Not String.IsNullOrEmpty(Me.m_Result)) Then
          Return Me.m_Result
        ElseIf (Me.Ex IsNot Nothing) Then
          Return Me.Ex.ToString()
        Else
          Return Nothing
        End If
      End Get
    End Property
  End Class
End Namespace