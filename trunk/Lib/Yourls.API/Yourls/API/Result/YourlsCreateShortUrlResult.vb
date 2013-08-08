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
Imports System.Runtime.Serialization
Imports System.Net
Imports NuGardt.API.Helper.JSON
Imports NuGardt.Yourls.API.Result.Element
Imports System.Text

Namespace Yourls.API.Result
  ''' <summary>
  ''' YOURLS API response for creating a short link.
  ''' </summary>
  ''' <remarks></remarks>
  <DataContract()>
  Public Class YourlsCreateShortUrlResult
    Implements IYourlsBaseResult

    'SUCESS:
    '{"status":"fail",
    '"code":"error:url",
    '"url":
    '{
    '"keyword":"2go8s",
    '"url":"http:\/\/www.nugardt.com",
    '"title":"Home | NuGardt Software",
    '"date":"2013-07-06 05:50:40",
    '"ip":"92.226.234.122",
    '"clicks":"1"
    '},
    '"message":"http:\/\/www.nugardt.com already exists in database",
    '"title":"Home | NuGardt Software",
    '"shorturl":"http:\/\/ls.nugardt.com\/2go8s",
    '"statusCode":200}

    'ERROR
    '{
    '"message":"Invalid username or password",
    '"errorCode":403,
    '"callback":""
    '}

    ''' <summary>
    ''' Returns or sets the status.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "status")>
    Public Status As String

    ''' <summary>
    ''' Returns or sets the code.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "code")>
    Public Code As String

    ''' <summary>
    ''' Returns or sets the title of the short link.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "title")>
    Public Title As String

    ''' <summary>
    ''' Returns or sets the short URL.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "shorturl")>
    Public ShortUrl As String

    ''' <summary>
    ''' Returns or sets the long URL.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "url")>
    Public Url As YourlsUrlData

#Region "IYourlsBaseResult"

    ''' <summary>
    ''' Returns or sets the message of the API call.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "message")>
    Public Property Message As String Implements IYourlsBaseResult.Message

    ''' <summary>
    ''' Returns or sets the status code of the API call. 200 = OK
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "statusCode")>
    Public Property StatusCode As Integer Implements IYourlsBaseResult.StatusCode

    ''' <summary>
    ''' Returns or sets the error code of the API call.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "errorCode", EmitDefaultValue := False)>
    Public Property ErrorCode As Integer Implements IYourlsBaseResult.ErrorCode

    ''' <summary>
    ''' Returns or sets the callback of the API call.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "callback", EmitDefaultValue := False)>
    Public Property Callback As String Implements IYourlsBaseResult.Callback

    <IgnoreDataMember()>
    Public Property ResponseRaw As String Implements IBaseResult.ResponseRaw

    <IgnoreDataMember()>
    Private Property CacheExpires As Date? Implements IBaseResult.CacheExpires

    Private Sub ReadHeader(Headers As WebHeaderCollection) Implements IBaseResult.ReadHeader
      '-
    End Sub

    Public ReadOnly Property HasError As Boolean
      Get
        Return (Me.ErrorCode <> 0)
      End Get
    End Property

    Public Function ToException() As Exception
      If Me.HasError Then
        Return New Exception(Me.Message)
      Else
        Return Nothing
      End If
    End Function

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Status: {0}{1}", Me.Status, vbCrLf)
        Call .AppendFormat("Code: {0}{1}", Me.Code, vbCrLf)
        Call .AppendFormat("Title: {0}{1}", Me.Title, vbCrLf)
        Call .AppendFormat("Short URL: {0}{1}", Me.ShortUrl, vbCrLf)
        If (Me.Url IsNot Nothing) Then Call .AppendLine(Me.Url.ToString())
      End With

      Return SB.ToString()
    End Function
  End Class
End Namespace