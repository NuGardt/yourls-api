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
Imports NuGardt.Yourls.API.Result
Imports System.Threading

Namespace Yourls.API
  Module StartUp
    Const ApiUrl = "http://yourshortdomain.com/yourls-api.php"
    Const Username = "<< Your username >>"
    Const Password = "<< Your password >>"
    Const Signature = "<< Your Signature>>"
    ReadOnly Timestamp As Nullable(Of DateTime) = DateTime.Now

    Const SyncTest As Boolean = True
    Const AsyncTest As Boolean = True

    Const CreateShortUrl As Boolean = True
    Const ExpandShortUrl As Boolean = True
    Const GetUrlStats As Boolean = True
    Const GetStats As Boolean = True
    Const GetDatabaseStats As Boolean = True

    Private AsyncCallsBusy As Int64

    Private Service As YourlsService
    Private MyAuth As IYourlsAuthentication
    Private ReadOnly MyKey As Guid = Guid.NewGuid()

    Sub Main()
      Dim CreateShortUrlResult As YourlsCreateShortUrlResult = Nothing
      Dim ExpandShortUrlResult As YourlsExpandShortUrlResult = Nothing
      Dim GetUrlStatsResult As YourlsGetUrlStatsResult = Nothing
      Dim GetStatsResult As YourlsGetStatsResult = Nothing
      Dim GetDatabaseStatsResult As YourlsGetDatabaseStatsResult = Nothing
      Dim AsyncResult As IAsyncResult = Nothing
      Dim Ex As Exception

      Service = New YourlsService(ApiUrl)

      'Username/Password
      MyAuth = New YourlsAuthentication(Username, Password)

      'Signature only
      'MyAuth = New YourlsAuthentication(Signature)

      'Signature with Timestamp (Temporary short URLs)
      'MyAuth = New YourlsAuthentication(Signature, Timestamp)

      'CreateShortUrl - Sync
      If SyncTest AndAlso CreateShortUrl Then
        Ex = Service.CreateShortUrl(LongUrl := "http://www.nugardt.com", Authentication := MyAuth, Result := CreateShortUrlResult, Keyword := "", Title := "")
        If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())
      End If

      'CreateShortUrl - ASync
      If AsyncTest AndAlso CreateShortUrl Then
        Call Interlocked.Increment(AsyncCallsBusy)
        AsyncResult = Service.CreateShortUrlBegin(Key := MyKey, LongUrl := "http://www.nugardt.com", Authentication := MyAuth, Callback := CreateShortUrlCallback, Keyword := "", Title := "")
      End If

      'ExpandShortUrl - Sync
      If SyncTest AndAlso ExpandShortUrl Then
        Ex = Service.ExpandShortUrl(ShortUrl := "http://ls.nugardt.com/2go8s", Authentication := MyAuth, Result := ExpandShortUrlResult)
        If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())
      End If

      'ExpandShortUrl - ASync
      If AsyncTest AndAlso ExpandShortUrl Then
        Call Interlocked.Increment(AsyncCallsBusy)
        AsyncResult = Service.ExpandShortUrlBegin(Key := MyKey, ShortUrl := "http://ls.nugardt.com/2go8s", Authentication := MyAuth, Callback := ExpandShortUrlCallback)
      End If

      'GetUrlStats - Sync
      If SyncTest AndAlso GetUrlStats Then
        Ex = Service.GetUrlStats(ShortUrl := "http://ls.nugardt.com/2go8s", Authentication := MyAuth, Result := GetUrlStatsResult)
        If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())
      End If

      'GetUrlStats - ASync
      If AsyncTest AndAlso GetUrlStats Then
        Call Interlocked.Increment(AsyncCallsBusy)
        AsyncResult = Service.GetUrlStatsBegin(Key := MyKey, ShortUrl := "http://ls.nugardt.com/2go8s", Authentication := MyAuth, Callback := GetUrlStatsCallback)
      End If

      'GetStats - Sync
      If SyncTest AndAlso GetStats Then
        Ex = Service.GetStats(Authentication := MyAuth, Filter := eYourlsFilter.Top, Limit := 34, Result := GetStatsResult)
        If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())
      End If

      'GetStats - ASync
      If AsyncTest AndAlso GetStats Then
        Call Interlocked.Increment(AsyncCallsBusy)
        AsyncResult = Service.GetStatsBegin(Key := MyKey, Authentication := MyAuth, Filter := eYourlsFilter.Top, Limit := 34, Callback := GetStatsCallback)
      End If

      'GetDatabaseStats - Sync
      If SyncTest AndAlso GetDatabaseStats Then
        Ex = Service.GetDatabaseStats(Authentication := MyAuth, Result := GetDatabaseStatsResult)
        If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())
      End If

      'GetDatabaseStats - ASync
      If AsyncTest AndAlso GetDatabaseStats Then
        Call Interlocked.Increment(AsyncCallsBusy)
        AsyncResult = Service.GetDatabaseStatsBegin(Key := MyKey, Authentication := MyAuth, Callback := GetDatabaseStatsCallback)
      End If

      Trace.WriteLine("Waiting on callbacks to complete...")
      Dim CallsBusy As Int64 = - 1

      Do Until CallsBusy = 0
        CallsBusy = Interlocked.Read(AsyncCallsBusy)

        Call Thread.Sleep(50)
      Loop
    End Sub

#Region "Callbacks"
    Private ReadOnly CreateShortUrlCallback As AsyncCallback = AddressOf iCreateShortUrlCallback

    Private Sub iCreateShortUrlCallback(ByVal Result As IAsyncResult)
      Dim Response As YourlsCreateShortUrlResult = Nothing
      Dim Key As Object = Nothing
      Dim Ex As Exception

      Ex = Service.CreateShortUrlEnd(Result, Key, Response)

      If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private ReadOnly ExpandShortUrlCallback As AsyncCallback = AddressOf iExpandShortUrlCallback

    Private Sub iExpandShortUrlCallback(ByVal Result As IAsyncResult)
      Dim Response As YourlsExpandShortUrlResult = Nothing
      Dim Key As Object = Nothing
      Dim Ex As Exception

      Ex = Service.ExpandShortUrlEnd(Result, Key, Response)

      If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private ReadOnly GetUrlStatsCallback As AsyncCallback = AddressOf iGetUrlStatsCallback

    Private Sub iGetUrlStatsCallback(ByVal Result As IAsyncResult)
      Dim Response As YourlsGetUrlStatsResult = Nothing
      Dim Key As Object = Nothing
      Dim Ex As Exception

      Ex = Service.GetUrlStatsEnd(Result, Key, Response)

      If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private ReadOnly GetStatsCallback As AsyncCallback = AddressOf iGetStatsCallback

    Private Sub iGetStatsCallback(ByVal Result As IAsyncResult)
      Dim Response As YourlsGetStatsResult = Nothing
      Dim Key As Object = Nothing
      Dim Ex As Exception

      Ex = Service.GetStatsEnd(Result, Key, Response)

      If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

    Private ReadOnly GetDatabaseStatsCallback As AsyncCallback = AddressOf iGetDatabaseStatsCallback

    Private Sub iGetDatabaseStatsCallback(ByVal Result As IAsyncResult)
      Dim Response As YourlsGetDatabaseStatsResult = Nothing
      Dim Key As Object = Nothing
      Dim Ex As Exception

      Ex = Service.GetDatabaseStatsEnd(Result, Key, Response)

      If (Ex IsNot Nothing) Then Call Trace.WriteLine(Ex.ToString())

      Call Interlocked.Decrement(AsyncCallsBusy)
    End Sub

#End Region
  End Module
End Namespace