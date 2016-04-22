REM #########################################################################################################################
Public Class FlacHeader
   REM ######################################################################################################################

   REM ######################################################################################################################
   Private LstBlkValue As String
   REM ######################################################################################################################

   REM ######################################################################################################################
   REM Beginn Structures just for Documentation
   REM *---------------------------------------------------------------------------------------------------------------------
   REM Source: https://xiph.org/flac/format.html
   REM ######################################################################################################################

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure StreamHdrflc
      Dim isFLAC As Boolean
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure BlckHeadrflc REM Flac - Blockheader
      Dim LstBlk As String       REM   1b is last Metadata-block
      Dim BlkTyp As String       REM   7b Block-Type Blocktype  0000000   0 Streaminfo
      REM                                                       0000001   1 Padding
      REM                                                       0000010   2 Application
      REM                                                       0000011   3 Seektable
      REM                                                       0000100   4 Vorbis_Comment
      REM                                                       0000101   5 Cuesheet
      REM                                                       0000110   6 Picture
      REM                                                       0000111   7
      REM                                                       ...         reserved
      REM                                                       1111110 126
      REM                                                       1111111 127 invalid, to avoid confusion with a frame sync code
      Dim BlkLng As String       REM  24b Length (in bytes) of metadata to follow
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MD0000000flc REM 0000000  0 Streaminfo
      Dim BlsMin As String       REM  16b The minimum block size (in samples) used in the stream.
      Dim BlsMax As String       REM  16b The maximum block size (in samples) used in the stream. (Minimum blocksize == maximum blocksize) implies a fixed-blocksize stream.
      REM                        FLAC specifies a minimum block size of 16 and a maximum block size of 65535, meaning the bit patterns corresponding to the numbers 0-15 in the minimum blocksize and maximum blocksize fields are invalid.
      Dim FrsMin As String       REM  24b The minimum frame size (in bytes) used in the stream. May be 0 to imply the value is not known
      Dim FrsMax As String       REM  24b The maximum frame size (in bytes) used in the stream. May be 0 to imply the value is not known
      Dim SmplRt As String       REM  20b Sample rate in Hz. Though 20 bits are available, the maximum sample rate is limited by the structure of frame headers to 655350Hz. Also, a value of 0 is invalid.
      Dim NbrChl As String       REM   3b (number of channels)-1. FLAC supports from 1 to 8 channels
      Dim SmplBt As String       REM   5b (bits per sample)-1. FLAC supports from 4 to 32 bits per sample. Currently the reference encoder and decoders only support up to 24 bits per sample.
      Dim SmplNb As String       REM  36b Total samples in stream. 'Samples' means inter-channel sample, i.e. one second of 44.1Khz audio will have 44100 samples regardless of the number of channels. A value of zero here means the number of total samples is unknown.
      Dim MD5Sgn As String       REM 128b MD5 signature of the unencoded audio data. This allows the decoder to determine if an error exists in the audio data even when the error does not result in an invalid bitstream.
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MD0000001flc REM 0000001  1 Padding
      Dim Paddin As String       REM   nb '0' bits (n must be a multiple of 8)
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MD0000010flc REM 0000010   2 Application
      Dim AplReg As String       REM  32b Registered application ID
      Dim AplDta As String       REM   nb Application data (n must be a multiple of 8)
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MD0000011flc REM 0000011   3 Seektable NOTE The number of seek points is implied by the metadata header 'length' field, i.e. equal to length / 18.
      Dim SekPnt As String       REM  18b One or more seek points.
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MDe000011flc REM                       NOTES For placeholder points, the second and third field values are undefined.
      REM                       Seek points within a table must be sorted in ascending order by sample number.
      REM                       Seek points within a table must be unique by sample number, with the exception of placeholder points.
      REM                       The previous two notes imply that there may be any number of placeholder points, but they must all occur at the end of the table.
      Dim FrsSmp As String       REM  64b Sample number of first sample in the target frame, or 0xFFFFFFFFFFFFFFFF for a placeholder point.
      Dim BytOff As String       REM  64b Offset (in bytes) from the first byte of the first frame header to the first byte of the target frame's header.
      Dim NbrSmp As String       REM  16b Number of samples in the target frame.
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MD0000100flc REM 0000100   4 Vorbis_Comment
      Dim VrbCmt As String       REM   nb Also known as FLAC tags, the contents of a vorbis comment packet as specified here (without the framing bit). Note that the vorbis comment spec allows for on the order of 2 ^ 64 bytes of data where as the FLAC metadata block is limited to 2 ^ 24 bytes. Given the stated purpose of vorbis comments, i.e. human-readable textual information, this limit is unlikely to be restrictive. Also note that the 32-bit field lengths are little-endian coded according to the vorbis spec, as opposed to the usual big-endian coding of fixed-length integers in the rest of FLAC.
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MD0000101flc REM  0000101   5 Cuesheet
      Dim MdCNbr As String       REM    <128*8> Media catalog number, in ASCII printable characters 0x20-0x7e. In general, the media catalog number may be 0 to 128 bytes long; any unused characters should be right-padded with NUL characters. For CD-DA, this is a thirteen digit number, followed by 115 NUL bytes.
      Dim NbrLiS As String       REM       <64> The number of lead-in samples. This field has meaning only for CD-DA cuesheets; for other uses it should be 0. For CD-DA, the lead-in is the TRACK 00 area where the table of contents is stored; more precisely, it is the number of samples from the first sample of the media to the first sample of the first index point of the first track. According to the Red Book, the lead-in must be silence and CD grabbing software does not usually store it; additionally, the lead-in must be at least two seconds but may be longer. For these reasons the lead-in length is stored here so that the absolute position of the first track can be computed. Note that the lead-in stored here is the number of samples up to the first index point of the first track, not necessarily to INDEX 01 of the first track; even the first track may have INDEX 00 data.
      Dim isCD As String         REM        <1> 1 if the CUESHEET corresponds to a Compact Disc, else 0.
      Dim Resvrd As String       REM  <7+258*8> Reserved. All bits must be set to zero.
      Dim NbrTrk As String       REM        <8> The number of tracks. Must be at least 1 (because of the requisite lead-out track). For CD-DA, this number must be no more than 100 (99 regular tracks and one lead-out track).
      REM CUESHEET_TRACK+ One or more tracks. A CUESHEET block is required to have a lead-out track; it is always the last track in the CUESHEET. For CD-DA, the lead-out track number must be 170 as specified by the Red Book, otherwise is must be 255.
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MDe000101flc REM CUESHEET_TRACK
      Dim TrkOff As String       REM       <64> Track offset in samples, relative to the beginning of the FLAC audio stream. It is the offset to the first index point of the track. (Note how this differs from CD-DA, where the track's offset in the TOC is that of the track's INDEX 01 even if there is an INDEX 00.) For CD-DA, the offset must be evenly divisible by 588 samples (588 samples = 44100 samples/sec * 1/75th of a sec).
      Dim TrkNbr As String       REM        <8> Track number. A track number of 0 is not allowed to avoid conflicting with the CD-DA spec, which reserves this for the lead-in. For CD-DA the number must be 1-99, or 170 for the lead-out; for non-CD-DA, the track number must for 255 for the lead-out. It is not required but encouraged to start with track 1 and increase sequentially. Track numbers must be unique within a CUESHEET.
      Dim TrISRC As String       REM     <12*8> Track ISRC. This is a 12-digit alphanumeric code; see here and here. A value of 12 ASCII NUL characters may be used to denote absence of an ISRC.
      Dim TrkTyp As String       REM        <1> The track type: 0 for audio, 1 for non-audio. This corresponds to the CD-DA Q-channel control bit 3.
      Dim TrkPEF As String       REM        <1> The pre-emphasis flag: 0 for no pre-emphasis, 1 for pre-emphasis. This corresponds to the CD-DA Q-channel control bit 5; see here.
      Dim Resvrd As String       REM   <6+13*8> Reserved. All bits must be set to zero.
      Dim TrkNIP As String       REM        <8> The number of track index points. There must be at least one index in every track in a CUESHEET except for the lead-out track, which must have zero. For CD-DA, this number may be no more than 100.
      REM CUESHEET_TRACK_INDEX+ For all tracks except the lead-out track, one or more track index points.
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MDe100101flc REM  CUESHEET_TRACK_INDEX
      Dim SmpOff As String       REM       <64> Offset in samples, relative to the track offset, of the index point. For CD-DA, the offset must be evenly divisible by 588 samples (588 samples = 44100 samples/sec * 1/75th of a sec). Note that the offset is from the beginning of the track, not the beginning of the audio data.
      Dim NIPNbr As String       REM        <8> The index point number. For CD-DA, an index number of 0 corresponds to the track pre-gap. The first index in a track must have a number of 0 or 1, and subsequently, index numbers must increase by 1. Index numbers must be unique within a track.
      Dim Resvrd As String       REM      <3*8> Reserved. All bits must be set to zero.
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------

   REM ----------------------------------------------------------------------------------------------------------------------
   Public Structure MD0000110flc REM  0000110   6 Picture METADATA_BLOCK_PICTURE
      Dim PicTyp As String       REM       <32> The picture type according to the ID3v2 APIC frame:
      REM             0 - Other
      REM             1 - 32x32 pixels 'file icon' (PNG only)
      REM             2 - Other file icon
      REM             3 - Cover (front)
      REM             4 - Cover (back)
      REM             5 - Leaflet page
      REM             6 - Media (e.g. label side of CD)
      REM             7 - Lead artist/lead performer/soloist
      REM             8 - Artist/performer
      REM             9 - Conductor
      REM            10 - Band/Orchestra
      REM            11 - Composer
      REM            12 - Lyricist/text writer
      REM            13 - Recording Location
      REM            14 - During recording
      REM            15 - During performance
      REM            16 - Movie/video screen capture
      REM            17 - A bright coloured fish
      REM            18 - Illustration
      REM            19 - Band/artist logotype
      REM            20 - Publisher/Studio logotype
      REM            Others are reserved and should not be used. There may only be one each of picture type 1 and 2 in a file.
      Dim MimLng As String       REM       <32> The length of the MIME type string in bytes.
      Dim MimTyp As String       REM      <n*8> The MIME type string, in printable ASCII characters 0x20-0x7e. The MIME type may also be --> to signify that the data part is a URL of the picture instead of the picture data itself.
      Dim DcrLng As String       REM       <32> The length of the description string in bytes.
      Dim DcrTxt As String       REM <DcrLng*8> The description of the picture, in UTF-8.
      Dim PicWdt As String       REM       <32> The width of the picture in pixels.
      Dim PicHgt As String       REM       <32> The height of the picture in pixels.
      Dim ColDpt As String       REM       <32> The color depth of the picture in bits-per-pixel.
      Dim ColCnt As String       REM       <32> For indexed-color pictures (e.g. GIF), the number of colors used, Or 0 for non-indexed pictures.
      Dim PicByt As String       REM       <32> The length of the picture data in bytes.
      Dim PicDta As String       REM            The binary picture data.
   End Structure
   REM ----------------------------------------------------------------------------------------------------------------------


   REM ######################################################################################################################
   REM End Structures just for Documentation
   REM ######################################################################################################################

   REM ######################################################################################################################
   Public Property LstBlk() As String

      REM -------------------------------------------------------------------------------------------------------------------
      REM * Gets the property value
      REM -------------------------------------------------------------------------------------------------------------------
      Get
         Dim InpArA() As String = {1, 2}
         InpArA(0) = 1
         InpArA(1) = 0
         Return InpArA.ToString()
      End Get
      REM -------------------------------------------------------------------------------------------------------------------

      REM -------------------------------------------------------------------------------------------------------------------
      REM * Sets the property value.
      REM -------------------------------------------------------------------------------------------------------------------
      Set(ByVal Value As String)
         LstBlkValue = Value
      End Set
      REM -------------------------------------------------------------------------------------------------------------------

   End Property
   REM ######################################################################################################################


End Class
REM #########################################################################################################################