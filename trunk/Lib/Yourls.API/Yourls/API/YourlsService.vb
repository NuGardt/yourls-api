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
Imports System.Net
Imports System.Security.Cryptography
Imports System.IO
Imports System.Runtime.Serialization.Json
Imports NuGardt.Yourls.API.Result
Imports System.Threading
Imports System.Text
Imports System.Web

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

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="ApiUrl">The URL to your YOURLS API. Example: "http://yoursite.com/yourls-api.php"</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ApiUrl As String)
      Me.ApiUrl = ApiUrl
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
        Dim SB As New StringBuilder

        Call SB.Append(Me.ApiUrl)
        Call SB.AppendFormat("?action={0}", ActionCreateShortUrl)
        Call SB.AppendFormat("&format={0}", ResponseFormat)
        Call SB.AppendFormat("&url={0}", HttpUtility.UrlEncode(LongUrl))

        If (Not String.IsNullOrEmpty(Keyword)) Then Call SB.AppendFormat("&keyword={0}", HttpUtility.UrlEncode(Keyword))
        If (Not String.IsNullOrEmpty(Title)) Then Call SB.AppendFormat("&title={0}", HttpUtility.UrlEncode(Title))

        Call SB.Append(GetAuthenticationDetails(Authentication))

        Result = QueryAndParse(Of YourlsCreateShortUrlResult)(SB.ToString(), Ex)
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
        Dim SB As New StringBuilder

        Call SB.Append(Me.ApiUrl)
        Call SB.AppendFormat("?action={0}", ActionCreateShortUrl)
        Call SB.AppendFormat("&format={0}", ResponseFormat)
        Call SB.AppendFormat("&url={0}", HttpUtility.UrlEncode(LongUrl))

        If (Not String.IsNullOrEmpty(Keyword)) Then Call SB.AppendFormat("&keyword={0}", HttpUtility.UrlEncode(Keyword))
        If (Not String.IsNullOrEmpty(Title)) Then Call SB.AppendFormat("&title={0}", HttpUtility.UrlEncode(Title))

        Call SB.Append(GetAuthenticationDetails(Authentication))

        Return QueryAndParseBegin(Key, SB.ToString(), Callback)
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
      Return QueryAndParseEnd(Result, Key, Response)
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
        Dim SB As New StringBuilder

        Call SB.Append(Me.ApiUrl)
        Call SB.AppendFormat("?action={0}", ActionExpandShortUrl)
        Call SB.AppendFormat("&format={0}", ResponseFormat)
        Call SB.AppendFormat("&shorturl={0}", HttpUtility.UrlEncode(ShortUrl))

        Call SB.Append(GetAuthenticationDetails(Authentication))

        Result = QueryAndParse(Of YourlsExpandShortUrlResult)(SB.ToString(), Ex)
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
        Dim SB As New StringBuilder

        Call SB.Append(Me.ApiUrl)
        Call SB.AppendFormat("?action={0}", ActionExpandShortUrl)
        Call SB.AppendFormat("&format={0}", ResponseFormat)
        Call SB.AppendFormat("&shorturl={0}", HttpUtility.UrlEncode(ShortUrl))

        Call SB.Append(GetAuthenticationDetails(Authentication))

        Return QueryAndParseBegin(Key, SB.ToString(), Callback)
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
      Return QueryAndParseEnd(Result, Key, Response)
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
        Dim SB As New StringBuilder

        Call SB.Append(Me.ApiUrl)
        Call SB.AppendFormat("?action={0}", ActionGetUrlStats)
        Call SB.AppendFormat("&format={0}", ResponseFormat)
        Call SB.AppendFormat("&shorturl={0}", HttpUtility.UrlEncode(ShortUrl))

        Call SB.Append(GetAuthenticationDetails(Authentication))

        Result = QueryAndParse(Of YourlsGetUrlStatsResult)(SB.ToString(), Ex)
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
        Dim SB As New StringBuilder

        Call SB.Append(Me.ApiUrl)
        Call SB.AppendFormat("?action={0}", ActionGetUrlStats)
        Call SB.AppendFormat("&format={0}", ResponseFormat)
        Call SB.AppendFormat("&shorturl={0}", HttpUtility.UrlEncode(ShortUrl))

        Call SB.Append(GetAuthenticationDetails(Authentication))

        Return QueryAndParseBegin(Key, SB.ToString(), Callback)
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
      Return QueryAndParseEnd(Result, Key, Response)
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

      Dim SB As New StringBuilder

      Call SB.Append(Me.ApiUrl)
      Call SB.AppendFormat("?action={0}", ActionGetStats)
      Call SB.AppendFormat("&format={0}", ResponseFormat)
      Call SB.AppendFormat("&filter={0}", HttpUtility.UrlEncode(Filter.ToString()))
      Call SB.AppendFormat("&limit={0}", Limit.ToString())

      Call SB.Append(GetAuthenticationDetails(Authentication))

      Result = QueryAndParse(Of YourlsGetStatsResult)(SB.ToString(), Ex)

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
      Dim SB As New StringBuilder

      Call SB.Append(Me.ApiUrl)
      Call SB.AppendFormat("?action={0}", ActionGetStats)
      Call SB.AppendFormat("&format={0}", ResponseFormat)
      Call SB.AppendFormat("&filter={0}", HttpUtility.UrlEncode(Filter.ToString()))
      Call SB.AppendFormat("&limit={0}", Limit.ToString())

      Call SB.Append(GetAuthenticationDetails(Authentication))

      Return QueryAndParseBegin(Key, SB.ToString(), Callback)
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
      Return QueryAndParseEnd(Result, Key, Response)
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

      Dim SB As New StringBuilder

      Call SB.Append(Me.ApiUrl)
      Call SB.AppendFormat("?action={0}", ActionGetDatabaseStats)
      Call SB.AppendFormat("&format={0}", ResponseFormat)

      Call SB.Append(GetAuthenticationDetails(Authentication))

      Result = QueryAndParse(Of YourlsGetDatabaseStatsResult)(SB.ToString(), Ex)

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
      Dim SB As New StringBuilder

      Call SB.Append(Me.ApiUrl)
      Call SB.AppendFormat("?action={0}", ActionGetDatabaseStats)
      Call SB.AppendFormat("&format={0}", ResponseFormat)

      Call SB.Append(GetAuthenticationDetails(Authentication))

      Return QueryAndParseBegin(Key, SB.ToString(), Callback)
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
      Return QueryAndParseEnd(Result, Key, Response)
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
          Call SB.AppendFormat("&signature={0}", HttpUtility.UrlEncode(Authentication.Signature))
        End If
      Else
        'Username/Password
        Call SB.AppendFormat("&username={0}", HttpUtility.UrlEncode(Authentication.Username))
        Call SB.AppendFormat("&password={0}", HttpUtility.UrlEncode(Authentication.Password))
      End If

      Return SB.ToString()
    End Function

    ''' <summary>
    ''' Queries the URL and returns the parsed response.
    ''' </summary>
    ''' <param name="URL">The query URL.</param>
    ''' <param name="Ex">Contains an Exception if an error occurred otherwise <c>Nothing</c>.</param>
    ''' <returns>Returns the response. May be <c>Nothing</c> if a critical error occurred.</returns>
    ''' <remarks></remarks>
    Private Function QueryAndParse(Of TItem As YourlsResult)(ByVal URL As String,
                                                             <Out()> ByRef Ex As Exception) As TItem
      Ex = Nothing
      Dim Result As TItem = Nothing
      Dim Request As WebRequest
      Dim ResponseStream As Stream
      Dim Response As WebResponse

      Try
        Request = HttpWebRequest.Create(URL)

        Response = Request.GetResponse()
        ResponseStream = Response.GetResponseStream()

        'Create Serilizer
        Dim Serializer As New DataContractJsonSerializer(GetType(TItem))

        'Deserialize
        Result = DirectCast(Serializer.ReadObject(ResponseStream), TItem)

        'Close stream
        Call ResponseStream.Close()
        Call ResponseStream.Dispose()

        'Close response
        Call Response.Close()
      Catch iEx As WebException
        Try
          Dim WebEx As WebException = iEx

          If (WebEx.Response IsNot Nothing) Then
            ResponseStream = WebEx.Response.GetResponseStream()

            'Create Serilizer
            Dim Serializer As New DataContractJsonSerializer(GetType(TItem))

            Result = DirectCast(Serializer.ReadObject(ResponseStream), TItem)

            'Close stream
            Call ResponseStream.Close()
            Call ResponseStream.Dispose()

            'Close response
            Call WebEx.Response.Close()
          End If

          Ex = iEx
        Catch iiEx As Exception
          Ex = iiEx
        End Try
      Catch iEx As Exception
        Ex = iEx
      End Try

      Return Result
    End Function

#Region "Class AsyncStateWithKey"

    Private NotInheritable Class AsyncStateWithKey
      Public ReadOnly Key As Object
      Public ReadOnly Request As WebRequest

      Public Sub New(ByVal Key As Object,
                     ByVal State As WebRequest)
        Me.Key = Key
        Me.Request = State
      End Sub
    End Class

#End Region

#Region "Class StateAsyncResult"

    Private NotInheritable Class StateAsyncResult
      Implements IAsyncResult

      Private ReadOnly m_AsyncState As Object

      Public Sub New(ByVal AsyncState As Object)
        Me.m_AsyncState = AsyncState
      End Sub

      Public ReadOnly Property AsyncState As Object Implements IAsyncResult.AsyncState
        Get
          Return Me.m_AsyncState
        End Get
      End Property

      Public ReadOnly Property AsyncWaitHandle As WaitHandle Implements IAsyncResult.AsyncWaitHandle
        Get
          Return Nothing
        End Get
      End Property

      Public ReadOnly Property CompletedSynchronously As Boolean Implements IAsyncResult.CompletedSynchronously
        Get
          Return True
        End Get
      End Property

      Public ReadOnly Property IsCompleted As Boolean Implements IAsyncResult.IsCompleted
        Get
          Return True
        End Get
      End Property
    End Class

#End Region

    ''' <summary>
    ''' Queries the URL without waiting for the response. The callback is invoked when the response is available.
    ''' </summary>
    ''' <param name="Key">Your key object.</param>
    ''' <param name="URL">The query URL.</param>
    ''' <param name="Callback">Address of callback method to deliver the response.</param>
    ''' <returns>Returns an <c>System.IAsyncResult</c>.</returns>
    ''' <remarks></remarks>
    Public Shared Function QueryAndParseBegin(ByVal Key As Object,
                                              ByVal URL As String,
                                              ByVal Callback As AsyncCallback) As IAsyncResult
      Dim Result As IAsyncResult
      Dim Request As WebRequest

      Try
        Request = HttpWebRequest.Create(URL)

        Result = Request.BeginGetResponse(Callback, New AsyncStateWithKey(Key, Request))
      Catch iEx As Exception
        Result = Callback.BeginInvoke(New StateAsyncResult(iEx), Nothing, Nothing)
      End Try

      Return Result
    End Function

    ''' <summary>
    ''' Parses the <c>IAsyncResult</c>.
    ''' </summary>
    ''' <typeparam name="TItem">The YOURLS result type.</typeparam>
    ''' <param name="Result">The <c>IAsyncResult</c> from the Callback.</param>
    ''' <param name="Key">Your key object.</param>
    ''' <param name="Response">Contains the parsed response if successful otherwise <c>Nothing</c>.</param>
    ''' <returns>Return an <c>System.Exception</c> if an error occurred otherwise <c>Nothing</c>.</returns>
    ''' <remarks></remarks>
    Public Function QueryAndParseEnd(Of TItem As YourlsResult)(ByVal Result As IAsyncResult,
                                                               <Out()> ByRef Key As Object,
                                                               <Out()> ByRef Response As TItem) As Exception
      Key = Nothing
      Response = Nothing
      Dim Ex As Exception = Nothing
      Dim State As AsyncStateWithKey
      Dim ResponseStream As Stream

      If (Result Is Nothing) Then
        Ex = New ArgumentNullException("Result")
      Else
        If (Not Result.IsCompleted) Then Call Result.AsyncWaitHandle.WaitOne()

        State = TryCast(Result.AsyncState, AsyncStateWithKey)

        If (State Is Nothing) Then
          Ex = New Exception("Invalid AsyncState.")
        Else
          Try
            Key = State.Key

            If (State.Request Is Nothing) Then
              Ex = New ArgumentNullException("Request")
            Else
              Dim WebResponse As WebResponse = State.Request.EndGetResponse(Result)
              ResponseStream = WebResponse.GetResponseStream()

              'Create Serilizer
              Dim Serializer As New DataContractJsonSerializer(GetType(TItem))

              'Deserialize
              Response = DirectCast(Serializer.ReadObject(ResponseStream), TItem)

              'Close stream
              Call ResponseStream.Close()
              Call ResponseStream.Dispose()

              'Close response
              Call WebResponse.Close()
            End If
          Catch iEx As WebException
            Try
              Dim WebEx As WebException = iEx

              If (WebEx.Response IsNot Nothing) Then
                ResponseStream = WebEx.Response.GetResponseStream()

                'Create Serilizer
                Dim Serializer As New DataContractJsonSerializer(GetType(TItem))

                Response = DirectCast(Serializer.ReadObject(ResponseStream), TItem)

                'Close stream
                Call ResponseStream.Close()
                Call ResponseStream.Dispose()

                'Close response
                Call WebEx.Response.Close()
              End If

              Ex = iEx
            Catch iiEx As Exception
              Ex = iiEx
            End Try
          Catch iEx As Exception
            Ex = iEx
          End Try
        End If
      End If

      Return Ex
    End Function
  End Class
End Namespace