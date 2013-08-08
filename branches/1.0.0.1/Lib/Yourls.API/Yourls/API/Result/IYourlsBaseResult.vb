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
Imports NuGardt.API.Helper.JSON
Imports System.Runtime.Serialization

Namespace Yourls.API.Result
  ''' <summary>
  ''' YOURLS API response.
  ''' </summary>
  ''' <remarks></remarks>
  Public Interface IYourlsBaseResult
    Inherits IBaseResult

    ''' <summary>
    ''' Returns or sets the message of the API call.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "message")>
    Property Message As String

    ''' <summary>
    ''' Returns or sets the status code of the API call. 200 = OK
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "statusCode")>
    Property StatusCode As Int32

    ''' <summary>
    ''' Returns or sets the error code of the API call.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "errorCode")>
    Property ErrorCode As Int32

    ''' <summary>
    ''' Returns or sets the callback of the API call.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "callback")>
    Property Callback As String
  End Interface
End Namespace