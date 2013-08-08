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
Imports System.Security.Cryptography
Imports NuGardt.API.Helper.JSON
Imports NuGardt.Yourls.API.Result
Imports System.Text

Namespace Yourls.API
  ''' <summary>
  ''' Class contains synchronous and asynchronous YOURLS API call functions.
  ''' </summary>
  ''' <remarks></remarks>
  Public Class YourlsService
    ''' <summary>
    ''' Returns the YOURLS API URL.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly ApiUrl As String

    Private Const ResponseFormat As String = "json"

    Private Const ActionCreateShortUrl As String = "shorturl"
    Private Const ActionExpandShortUrl As String = "expand"
    Private Const ActionGetUrlStats As String = "url-stats"
    Private Const ActionGetStats As String = "stats"
    Private Const ActionGetDatabaseStats As String = "db-stats"

    Private ReadOnly Query As JsonHelper(Of IYourlsBaseResult)

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="ApiUrl">The URL to your YOURLS API. Example: "http://yoursite.com/yourls-api.php"</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ApiUrl As String)
      Me.ApiUrl = ApiUrl

      Me.Query = New JsonHelper(Of IYourlsBaseResult)(False)
    End Sub

#Region "Function CreateShortUrl"

    ''' <summary>
    ''' Create a short URL from your long URL.
    ''' </summary>
    ''' <param name="LongUrl">The long URL to shorten.</param>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Result">Contains the result. Can be <c>Nothing</c>.</param>
    ''' <param name="Keyword">Keyword to use for the short name.</param>
    ''' <param name="Title">Title of the URL.</param>
    ''' <returns>Returns an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function CreateShortUrl(ByVal LongUrl As String,
                                   ByVal Authentication As IYourlsAuthentication,
                                   <Out()> ByRef Result As YourlsCreateShortUrlResult,
                                   Optional ByVal Keyword As String = Nothing,
                                   Optional ByVal Title As String = Nothing) As Exception
      Result = Nothing
      Dim Ex As Exception = Nothing

      If String.IsNullOrEmpty(LongUrl) Then
        Ex = New ArgumentNullException("LongUrl")
      ElseIf (Authentication Is Nothing) Then
        Ex = New ArgumentNullException("Authentication")
      Else
        Dim RequestData As New StringBuilder

        Call RequestData.AppendFormat("format={0}", ResponseFormat)
        Call RequestData.AppendFormat("&url={0}", LongUrl)

        If (Not String.IsNullOrEmpty(Keyword)) Then Call RequestData.AppendFormat("&keyword={0}", Keyword)
        If (Not String.IsNullOrEmpty(Title)) Then Call RequestData.AppendFormat("&title={0}", Title)

        Call RequestData.Append(GetAuthenticationDetails(Authentication))

        Result = Me.Query.QueryAndParse(Of YourlsCreateShortUrlResult)(eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionCreateShortUrl), RequestData.ToString(), Ex := Ex)
      End If

      Return Ex
    End Function

    ''' <summary>
    ''' Create a short URL from your long URL.
    ''' </summary>
    ''' <param name="Key">Your own Key for tracking asynchronous calls.</param>
    ''' <param name="LongUrl">The long URL to shorten.</param>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Callback">Method to call on completion or failure.</param>
    ''' <param name="Keyword">Keyword to use for the short name.</param>
    ''' <param name="Title">Title of the URL.</param>
    ''' <returns>Returns an <c>System.IAsyncResult</c>.</returns>
    ''' <remarks></remarks>
    Public Function CreateShortUrlBegin(ByVal Key As Object,
                                        ByVal LongUrl As String,
                                        ByVal Authentication As IYourlsAuthentication,
                                        ByVal Callback As AsyncCallback,
                                        Optional ByVal Keyword As String = Nothing,
                                        Optional ByVal Title As String = Nothing) As IAsyncResult
      If String.IsNullOrEmpty(LongUrl) Then
        Return Callback.BeginInvoke(Nothing, Nothing, New NullReferenceException("LongUrl"))
      ElseIf (Authentication Is Nothing) Then
        Return Callback.BeginInvoke(Nothing, Nothing, New NullReferenceException("Authentication"))
      Else
        Dim RequestData As New StringBuilder

        Call RequestData.AppendFormat("format={0}", ResponseFormat)
        Call RequestData.AppendFormat("&url={0}", LongUrl)

        If (Not String.IsNullOrEmpty(Keyword)) Then Call RequestData.AppendFormat("&keyword={0}", Keyword)
        If (Not String.IsNullOrEmpty(Title)) Then Call RequestData.AppendFormat("&title={0}", Title)

        Call RequestData.Append(GetAuthenticationDetails(Authentication))

        Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionCreateShortUrl), RequestData.ToString(), Callback)
      End If
    End Function

    ''' <summary>
    ''' Gets the response after the callback.
    ''' </summary>
    ''' <param name="Result">Asynchronous result</param>
    ''' <param name="Key">Contains your key for tracking asynchronous calls.</param>
    ''' <param name="Response">Contains response if applicable.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function CreateShortUrlEnd(ByVal Result As IAsyncResult,
                                      <Out()> ByRef Key As Object,
                                      <Out()> ByRef Response As YourlsCreateShortUrlResult) As Exception
      Return Me.Query.QueryAndParseEnd(Result, Key, Response)
    End Function

#End Region

#Region "Function ExpandShortUrl"

    ''' <summary>
    ''' Get the long URL of a short URL or keyword.
    ''' </summary>
    ''' <param name="ShortUrl">The short URL or keyword.</param>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Result">Contains the result. Can be <c>Nothing</c>.</param>
    ''' <returns>Returns an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function ExpandShortUrl(ByVal ShortUrl As String,
                                   ByVal Authentication As IYourlsAuthentication,
                                   <Out()> ByRef Result As YourlsExpandShortUrlResult) As Exception
      Result = Nothing
      Dim Ex As Exception = Nothing

      If String.IsNullOrEmpty(ShortUrl) Then
        Ex = New ArgumentNullException("ShortUrl")
      Else
        Dim RequestData As New StringBuilder

        Call RequestData.AppendFormat("format={0}", ResponseFormat)
        Call RequestData.AppendFormat("&shorturl={0}", ShortUrl)

        Call RequestData.Append(GetAuthenticationDetails(Authentication))

        Result = Me.Query.QueryAndParse(Of YourlsExpandShortUrlResult)(eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionExpandShortUrl), RequestData.ToString(), Ex := Ex)
      End If

      Return Ex
    End Function

    ''' <summary>
    ''' Get the long URL of a short URL or keyword.
    ''' </summary>
    ''' <param name="Key">Your own Key for tracking asynchronous calls.</param>
    ''' <param name="ShortUrl">The short URL or keyword.</param>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Callback">Method to call on completion or failure.</param>
    ''' <returns>Returns an <c>System.IAsyncResult</c>.</returns>
    ''' <remarks></remarks>
    Public Function ExpandShortUrlBegin(ByVal Key As Object,
                                        ByVal ShortUrl As String,
                                        ByVal Authentication As IYourlsAuthentication,
                                        ByVal Callback As AsyncCallback) As IAsyncResult
      If String.IsNullOrEmpty(ShortUrl) Then
        Return Callback.BeginInvoke(Nothing, Nothing, New NullReferenceException("Url"))
      Else
        Dim RequestData As New StringBuilder

        Call RequestData.AppendFormat("format={0}", ResponseFormat)
        Call RequestData.AppendFormat("&shorturl={0}", ShortUrl)

        Call RequestData.Append(GetAuthenticationDetails(Authentication))

        Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionExpandShortUrl), RequestData.ToString(), Callback)
      End If
    End Function

    ''' <summary>
    ''' Gets the response after the callback.
    ''' </summary>
    ''' <param name="Result">Asynchronous result</param>
    ''' <param name="Key">Contains your key for tracking asynchronous calls.</param>
    ''' <param name="Response">Contains response if applicable.</param>
    ''' <returns>Returns an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function ExpandShortUrlEnd(ByVal Result As IAsyncResult,
                                      <Out()> ByRef Key As Object,
                                      <Out()> ByRef Response As YourlsExpandShortUrlResult) As Exception
      Return Me.Query.QueryAndParseEnd(Result, Key, Response)
    End Function

#End Region

#Region "Function GetUrlStats"

    ''' <summary>
    ''' Get the URL statistics from a short URL or keyword.
    ''' </summary>
    ''' <param name="ShortUrl">The short URL or keyword.</param>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Result">Contains the result. Can be <c>Nothing</c>.</param>
    ''' <returns>Returns an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetUrlStats(ByVal ShortUrl As String,
                                ByVal Authentication As IYourlsAuthentication,
                                <Out()> ByRef Result As YourlsGetUrlStatsResult) As Exception
      Result = Nothing
      Dim Ex As Exception = Nothing

      If String.IsNullOrEmpty(ShortUrl) Then
        Ex = New ArgumentNullException("ShortUrl")
      Else
        Dim RequestData As New StringBuilder

        Call RequestData.AppendFormat("format={0}", ResponseFormat)
        Call RequestData.AppendFormat("&shorturl={0}", ShortUrl)

        Call RequestData.Append(GetAuthenticationDetails(Authentication))

        Result = Me.Query.QueryAndParse(Of YourlsGetUrlStatsResult)(eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionGetUrlStats), RequestData.ToString(), Ex := Ex)
      End If

      Return Ex
    End Function

    ''' <summary>
    ''' Get the URL statistics from a short URL or keyword.
    ''' </summary>
    ''' <param name="Key">Your own Key for tracking asynchronous calls.</param>
    ''' <param name="ShortUrl">The short URL or keyword.</param>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Callback">Method to call on completion or failure.</param>
    ''' <returns>Returns an <c>System.IAsyncResult</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetUrlStatsBegin(ByVal Key As Object,
                                     ByVal ShortUrl As String,
                                     ByVal Authentication As IYourlsAuthentication,
                                     ByVal Callback As AsyncCallback) As IAsyncResult
      If String.IsNullOrEmpty(ShortUrl) Then
        Return Callback.BeginInvoke(Nothing, Nothing, New NullReferenceException("Url"))
      Else
        Dim RequestData As New StringBuilder

        Call RequestData.AppendFormat("format={0}", ResponseFormat)
        Call RequestData.AppendFormat("&shorturl={0}", ShortUrl)

        Call RequestData.Append(GetAuthenticationDetails(Authentication))

        Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionGetUrlStats), RequestData.ToString(), Callback)
      End If
    End Function

    ''' <summary>
    ''' Gets the response after the callback.
    ''' </summary>
    ''' <param name="Result">Asynchronous result</param>
    ''' <param name="Key">Contains your key for tracking asynchronous calls.</param>
    ''' <param name="Response">Contains response if applicable.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetUrlStatsEnd(ByVal Result As IAsyncResult,
                                   <Out()> ByRef Key As Object,
                                   <Out()> ByRef Response As YourlsGetUrlStatsResult) As Exception
      Return Me.Query.QueryAndParseEnd(Result, Key, Response)
    End Function

#End Region

#Region "Function GetStats"

    ''' <summary>
    ''' Get URL statistics.
    ''' </summary>
    ''' <param name="Filter">URL based on Filter</param>
    ''' <param name="Limit">The number of links to return. (Issue: NYA-7: Implementation limits result to 25 links)</param>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Result">Contains the result. Can be <c>Nothing</c>.</param>
    ''' <returns>Returns an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetStats(ByVal Filter As eYourlsFilter,
                             ByVal Limit As Int32,
                             ByVal Authentication As IYourlsAuthentication,
                             <Out()> ByRef Result As YourlsGetStatsResult) As Exception
      Dim Ex As Exception = Nothing

      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("format={0}", ResponseFormat)
      Call RequestData.AppendFormat("&filter={0}", Filter.ToString())
      Call RequestData.AppendFormat("&limit={0}", Limit.ToString())

      Call RequestData.Append(GetAuthenticationDetails(Authentication))

      Result = Me.Query.QueryAndParse(Of YourlsGetStatsResult)(eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionGetStats), RequestData.ToString(), Ex := Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Get URL statistics.
    ''' </summary>
    ''' <param name="Key">Your own Key for tracking asynchronous calls.</param>
    ''' <param name="Filter">URL based on Filter</param>
    ''' <param name="Limit">The number of links to return. (Issue: NYA-7: Implementation limits result to 25 links)</param>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Callback">Method to call on completion or failure.</param>
    ''' <returns>Returns an <c>System.IAsyncResult</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetStatsBegin(ByVal Key As Object,
                                  ByVal Filter As eYourlsFilter,
                                  ByVal Limit As Int32,
                                  ByVal Authentication As IYourlsAuthentication,
                                  ByVal Callback As AsyncCallback) As IAsyncResult
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("format={0}", ResponseFormat)
      Call RequestData.AppendFormat("&filter={0}", Filter.ToString())
      Call RequestData.AppendFormat("&limit={0}", Limit.ToString())

      Call RequestData.Append(GetAuthenticationDetails(Authentication))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionGetStats), RequestData.ToString(), Callback)
    End Function

    ''' <summary>
    ''' Gets the response after the callback.
    ''' </summary>
    ''' <param name="Result">Asynchronous result</param>
    ''' <param name="Key">Contains your key for tracking asynchronous calls.</param>
    ''' <param name="Response">Contains response if applicable.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetStatsEnd(ByVal Result As IAsyncResult,
                                <Out()> ByRef Key As Object,
                                <Out()> ByRef Response As YourlsGetStatsResult) As Exception
      Return Me.Query.QueryAndParseEnd(Result, Key, Response)
    End Function

#End Region

#Region "Function GetDatabaseStats"

    ''' <summary>
    ''' Get database statistics.
    ''' </summary>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Result">Contains the result. Can be <c>Nothing</c>.</param>
    ''' <returns>Returns an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetDatabaseStats(ByVal Authentication As IYourlsAuthentication,
                                     <Out()> ByRef Result As YourlsGetDatabaseStatsResult) As Exception
      Dim Ex As Exception = Nothing
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("format={0}", ResponseFormat)
      Call RequestData.Append(GetAuthenticationDetails(Authentication))

      Result = Me.Query.QueryAndParse(Of YourlsGetDatabaseStatsResult)(eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionGetDatabaseStats), RequestData.ToString(), Ex := Ex)

      Return Ex
    End Function

    ''' <summary>
    ''' Get database statistics.
    ''' </summary>
    ''' <param name="Key">Your own Key for tracking asynchronous calls.</param>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <param name="Callback">Method to call on completion or failure.</param>
    ''' <returns>Returns an <c>System.IAsyncResult</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetDatabaseStatsBegin(ByVal Key As Object,
                                          ByVal Authentication As IYourlsAuthentication,
                                          ByVal Callback As AsyncCallback) As IAsyncResult
      Dim RequestData As New StringBuilder

      Call RequestData.AppendFormat("format={0}", ResponseFormat)
      Call RequestData.Append(GetAuthenticationDetails(Authentication))

      Return Me.Query.QueryAndParseBegin(Key, eRequestMethod.Post, String.Format("{0}?action={1}", Me.ApiUrl, ActionGetDatabaseStats), RequestData.ToString(), Callback)
    End Function

    ''' <summary>
    ''' Gets the response after the callback.
    ''' </summary>
    ''' <param name="Result">Asynchronous result</param>
    ''' <param name="Key">Contains your key for tracking asynchronous calls.</param>
    ''' <param name="Response">Contains response if applicable.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function GetDatabaseStatsEnd(ByVal Result As IAsyncResult,
                                        <Out()> ByRef Key As Object,
                                        <Out()> ByRef Response As YourlsGetDatabaseStatsResult) As Exception
      Return Me.Query.QueryAndParseEnd(Result, Key, Response)
    End Function

#End Region

    ''' <summary>
    ''' Returns the enocoded authentication details.
    ''' </summary>
    ''' <param name="Authentication">Authentication details.</param>
    ''' <returns>Returns the enocoded authentication details.</returns>
    ''' <remarks></remarks>
    Private Shared Function GetAuthenticationDetails(ByVal Authentication As IYourlsAuthentication) As String
      Dim SB As New StringBuilder

      If (Not String.IsNullOrEmpty(Authentication.Signature)) Then
        'Signature
        If Authentication.UnixTimestamp.HasValue Then
          Dim HashedSignature As String
          Dim MD5 As MD5 = MD5.Create

          HashedSignature = BitConverter.ToString(MD5.ComputeHash(Encoding.UTF8.GetBytes(String.Format("{0}{1}", Authentication.UnixTimestamp.Value, Authentication.Signature)))).Replace("-", "").ToLower()

          Call SB.AppendFormat("&signature={0}&timestamp={1}", HashedSignature, Authentication.UnixTimestamp.Value.ToString())
        Else
          Call SB.AppendFormat("&signature={0}", Authentication.Signature)
        End If
      Else
        'Username/Password
        Call SB.AppendFormat("&username={0}", Authentication.Username)
        Call SB.AppendFormat("&password={0}", Authentication.Password)
      End If

      Return SB.ToString()
    End Function
  End Class
End Namespace