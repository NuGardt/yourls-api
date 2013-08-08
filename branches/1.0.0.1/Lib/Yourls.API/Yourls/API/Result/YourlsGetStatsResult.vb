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
  ''' YOURLS API response.
  ''' </summary>
  ''' <remarks></remarks>
  <DataContract()>
  Public Class YourlsGetStatsResult
    Implements IYourlsBaseResult

    'SUCESS
    '{"links":
    '{
    '"link_1":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/2go8s",
    '"url":"http:\/\/www.nugardt.com",
    '"title":"Home | NuGardt Software",
    '"timestamp":"2013-07-06 05:50:40",
    '"ip":"92.226.234.122",
    '"clicks":"1"
    '},
    '"link_2":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/95tyl",
    '"url":"https:\/\/twitter.com\/NuGardt",
    '"title":"NuGardt Software (NuGardt) auf Twitter",
    '"timestamp":"2013-07-06 05:52:52",
    '"ip":"92.226.234.122",
    '"clicks":"1"
    '},
    '"link_3":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/5kzkb",
    '"url":"http:\/\/jira.nugardt.com:9080",
    '"title":"http:\/\/jira.nugardt.com:9080",
    '"timestamp":"2013-07-06 05:52:02",
    '"ip":"92.226.234.122",
    '"clicks":"0"
    '},
    '"link_4":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/f0yk0",
    '"url":"https:\/\/jira.nugardt.com:9443",
    '"title":"https:\/\/jira.nugardt.com:9443",
    '"timestamp":"2013-07-06 05:52:19",
    '"ip":"92.226.234.122",
    '"clicks":"0"
    '}
    ',"link_5":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/o1628",
    '"url":"http:\/\/www.seismovision.net",
    '"title":"Seismovision - The Demo Player",
    '"timestamp":"2013-07-06 05:52:29",
    '"ip":"92.226.234.122",
    '"clicks":"0"
    '}
    '},
    '"stats":
    '{
    '"total_links":"5",
    '"total_clicks":"2"
    '}
    ',"statusCode":200,
    '"message":"success"
    '}

    'ERROR
    '{
    '"message":"Invalid username or password",
    '"errorCode":403,
    '"callback":""
    '}

    ''' <summary>
    ''' Returns or sets the links.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "links")>
    Public Links As YourlsLinks

    ''' <summary>
    ''' Returns or sets the Database statistics.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "stats")>
    Public Stats As YourlsDatabaseStats

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
        If (Me.Links IsNot Nothing) Then Call .AppendLine(Me.Links.ToString())
        If (Me.Stats IsNot Nothing) Then Call .AppendLine(Me.Stats.ToString())
      End With

      Return SB.ToString()
    End Function
  End Class
End Namespace